using Extentions;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Gateways
{
    public class GatewayView : Transformable
    {
        [SerializeField] private bool _invert;
        
        [Inject] private IPlayerCameraService PlayerCamera { get; set; }

        private void Update()
        {
            LookAtPlayer();
        }

        private void LookAtPlayer()
        {
            Vector3 direction = PlayerCamera.CameraTransform.forward;
            if (_invert)
                direction = -direction;
            Transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}