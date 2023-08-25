using System;
using Extentions.Factory;
using Gameplay.Gateways;
using Gameplay.Surfaces;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerGloves : MonoBehaviour
    {
        private PlayerVision _vision;

        public PlayerVision Vision => _vision ??= GetComponent<PlayerVision>();
        
        [Inject] private GatewaySystem GatewaySystem { get; set; }

        public void TryOpenGateway(int index)
        {
            LayerMask mask = LayerMask.GetMask("Obstacle");
            if ( ! Physics.Raycast(Vision.LookRay, out RaycastHit hit, Single.MaxValue, mask))
                return;
            
            if ( ! hit.collider.TryGetComponent(out Brick brick) || ! brick.Conductive)
                return;
            
            Vector3 sphereCenter = hit.point + hit.normal * 0.5f;
            if (Physics.OverlapSphere(sphereCenter, 0.45f, mask).Length > 0)
                return;
            
            GatewaySystem.OpenGateway(index, hit.point, hit.normal, hit.collider.transform);
        }
    }
}