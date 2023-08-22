using System;
using Extentions;
using Extentions.Pause;
using Input;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Action = System.Action;

namespace Gameplay.Player
{
    public class PlayerControls : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _linearJoystickCurve;
        [SerializeField] private AnimationCurve _exponentialJoystickCurve;
        [SerializeField] private AnimationCurve _dualzoneJoystickCurve;
        private GameplayActions _actions;
        public SettingsConfig Config { get; private set; }

        public Vector2 CameraDelta
        {
            get
            {
                Vector2 input = _actions.Player.Camera.ReadValue<Vector2>();

                if (DeviceWatcher.CurrentInputScheme == InputScheme.KeyboardMouse)
                {
                    input *= new Vector2(Config.KeyboardMouse.GetSettingValue("sensitivity x"), Config.KeyboardMouse.GetSettingValue("sensitivity y"));
                    return input;
                }

                input = ApplyDeadzone(input);
                input = ApplyResponseCurve(input);
                input = InvertAxes(input);
                input = ApplySensivity(input);
                input = ApplyGyro(input);
                
                return input;

                
                Vector2 ApplyDeadzone(Vector2 originInput)
                {
                    if (originInput.magnitude * 100 < Config.Gamepad.GetSettingValue("deadzone"))
                        originInput = Vector2.zero;
                    return originInput;
                }

                Vector2 ApplyResponseCurve(Vector2 originInput)
                {
                    AnimationCurve curve = Config.Gamepad.GetSettingValue("response curve") switch
                    {
                        1 => _exponentialJoystickCurve,
                        2 => _dualzoneJoystickCurve,
                        _ => _linearJoystickCurve,
                    };
                    float magnitude = curve.Evaluate(originInput.magnitude);
                    originInput.SetMagnitude(magnitude);
                    return originInput.SetMagnitude(magnitude);
                }

                Vector2 InvertAxes(Vector2 originInput)
                {
                    if (Config.Gamepad.IsSettingToggled("invert y"))
                        originInput = originInput.WithY(-originInput.y);
                    if (Config.Gamepad.IsSettingToggled("invert x"))
                        originInput = originInput.WithX(-originInput.x);
                    return originInput;
                }

                Vector2 ApplySensivity(Vector2 originInput)
                {
                    originInput *= new Vector2(Config.Gamepad.GetSettingValue("sensitivity x"), Config.Gamepad.GetSettingValue("sensitivity y")) * 6;
                    return originInput;
                }
                
                Vector2 ApplyGyro(Vector2 originInput)
                {
                    if (!Config.Gyro.IsSettingToggled("enabled"))
                        return originInput;
                    
                    Vector2 gyroAxes = GetScreenGyroDelta(Config.Gyro.IsSettingToggled("world space"));
                    if (gyroAxes.magnitude < Config.Gyro.GetSettingValue("deadzone") * 0.015f)
                        return originInput;
                    
                    gyroAxes *= new Vector2(Config.Gyro.GetSettingPercent("scale x"), Config.Gyro.GetSettingPercent("scale y"));
                    originInput += gyroAxes * 270;
                    return originInput;
                }
            }
        }

        public Vector2 MovementDelta
        {
            get
            {
                Vector2 input = _actions.Player.Move.ReadValue<Vector2>();
                return Vector2.ClampMagnitude(input, 1);
            }
        }

        public event Action JumpPressed;
        public event Action<int> GatewayPressed;
        
        [Inject] private DeviceWatcher DeviceWatcher { get; set; }
        [Inject] private IPauseReadService Pause { get; set; }

        [Inject]
        private void Construct(GameplayActions actions, Settings.Settings settings)
        {
            _actions = actions;
            _actions.Enable();
            
            _actions.Player.Jump.started += PressJump;
            _actions.Player.Gateway1.performed += PressGateway1;
            _actions.Player.Gateway2.performed += PressGateway2;

            Config = settings.Config;
        }

        private void PressJump(InputAction.CallbackContext _)
        {
            if (Pause.IsPaused)
                return;
            JumpPressed?.Invoke();
        }

        private void PressGateway1(InputAction.CallbackContext _)
        {
            if (Pause.IsPaused)
                return;
            GatewayPressed?.Invoke(0);
        }
        
        private void PressGateway2(InputAction.CallbackContext _)
        {
            if (Pause.IsPaused)
                return;
            GatewayPressed?.Invoke(1);
        }

        private void OnDestroy()
        {
            _actions.Player.Jump.started -= PressJump;
            _actions.Player.Gateway1.performed -= PressGateway1;
            _actions.Player.Gateway2.performed -= PressGateway2;
        }
        
        private Vector2 GetScreenGyroDelta(bool worldSpace)
        {
            Vector3 gyro = DualshockMotion.RawGyro;
            float xAxisInfluence = DualshockMotion.Accelerometer.y;
            bool invertZ = DualshockMotion.Accelerometer.z > 0;
            
            Vector2 delta = new Vector2(-gyro.y, gyro.x);
            if (worldSpace)
            {
                delta.x = -gyro.y * xAxisInfluence + (gyro.z * (1 - xAxisInfluence)) * (invertZ ? -1 : 1);
            }
            return delta;
        }
    }
}