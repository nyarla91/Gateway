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


        public event Action JumpPressed;
        public event Action InteractPressed;
        public event Action<int> GatewayPressed;

        [Inject] private DeviceWatcher DeviceWatcher { get; set; }

        [Inject] private IPauseReadService Pause { get; set; }

        [Inject]
        private void Construct(GameplayActions actions, Settings.Settings settings)
        {
            _actions = actions;
            _actions.Enable();
            
            _actions.Player.Jump.performed += PressJump;
            _actions.Player.Interact.performed += PressInteract;
            _actions.Player.Gateway1.performed += PressGateway1;
            _actions.Player.Gateway2.performed += PressGateway2;

            Config = settings.Config;
        }

        public Vector2 GetMovementDelta()
        {
            Vector2 input = _actions.Player.Move.ReadValue<Vector2>();
            return Vector2.ClampMagnitude(input, 1);
        }
        
        public Vector2 GetCameraDelta()
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
        }

        private void PressJump(InputAction.CallbackContext _)
        {
            if (Pause.IsPaused)
                return;
            JumpPressed?.Invoke();
        }

        private void PressInteract(InputAction.CallbackContext _)
        {
            if (Pause.IsPaused)
                return;
            InteractPressed?.Invoke();
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
            _actions.Player.Jump.performed -= PressJump;
            _actions.Player.Interact.performed -= PressInteract;
            _actions.Player.Gateway1.performed -= PressGateway1;
            _actions.Player.Gateway2.performed -= PressGateway2;
        }
    }
}