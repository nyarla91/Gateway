using System;
using UnityEngine;

namespace Gameplay.Entity.Player
{
    public class PlayerPresenter : MonoBehaviour
    {
        [SerializeField] private PlayerControls _controls;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerVision _vision;

        private void Awake()
        {
            _controls.JumpPressed += _movement.TryJump;
            _movement.MovementInputBind += () => _controls.MovementDelta;
            _vision.CameraRotationBind += () => _controls.CameraDelta;
        }
    }
}