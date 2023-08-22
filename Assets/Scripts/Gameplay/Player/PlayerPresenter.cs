using Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerControls _controls;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerVision _vision;
        [SerializeField] private PlayerGloves _gloves;
        [SerializeField] private PlayerInteraction _interaction;

        [Inject] private InteractablePrompt InteractablePrompt { get; set; }
        
        private void Awake()
        {
            _controls.JumpPressed += _movement.TryJump;
            _controls.InteractPressed += _interaction.TryInteract;
            _controls.GatewayPressed += _gloves.OpenGateway;
            _movement.MovementInputBind += () => _controls.MovementDelta;
            _vision.CameraRotationBind += () => _controls.CameraDelta;
            InteractablePrompt.TargetBind += () => _interaction.Target;
            InteractablePrompt.InteractableScreenPosition += _interaction.InteractableScreenPosition;
        }
    }
}