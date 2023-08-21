using UnityEngine;

namespace Gameplay.Entity.Player
{
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerControls _controls;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerVision _vision;
        [SerializeField] private PlayerGloves _gloves;

        private void Awake()
        {
            _controls.JumpPressed += _movement.TryJump;
            _controls.GatewayPressed += _gloves.OpenGateway;
            _movement.MovementInputBind += () => _controls.MovementDelta;
            _vision.CameraRotationBind += () => _controls.CameraDelta;
        }
    }
}