using System;
using Extentions.Factory;
using Gameplay.Gateways;
using UnityEngine;
using Zenject;

namespace Gameplay.Entity.Player
{
    public class PlayerGloves : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gatewaysPrefabs = new GameObject[2];

        [Inject] private ContainerFactory ContainerFactory { get; set; }
        
        private readonly Gateway[] _openGateways = new Gateway[2];
        private PlayerVision _vision;

        public PlayerVision Vision => _vision ??= GetComponent<PlayerVision>();
        
        public void OpenGateway(int index)
        {
            LayerMask mask = LayerMask.GetMask("Obstacle");
            if ( ! Physics.Raycast(Vision.LookRay, out RaycastHit hit, Single.MaxValue, mask))
                return;

            Vector3 sphereCenter = hit.point + hit.normal * 0.5f;
            if (Physics.OverlapSphere(sphereCenter, 0.45f, mask).Length > 0)
                return;

            Vector3 position = hit.point;
            Quaternion rotation = Quaternion.LookRotation(hit.normal, Vector3.up);
            Transform parent = hit.collider.transform;

            Gateway currentGateway = _openGateways[index];
            if (currentGateway == null)
            {
                currentGateway = _openGateways[index] = ContainerFactory.Instantiate<Gateway>(_gatewaysPrefabs[index], position);
                Gateway otherGateway = _openGateways[GetOppositeIndex(index)]; 
                if (otherGateway != null)
                    currentGateway.Bind(otherGateway);
            }

            currentGateway.Transform.position = position;
            currentGateway.Transform.rotation = rotation;
            currentGateway.Transform.parent = null;
            currentGateway.Transform.localScale = Vector3.one;
            currentGateway.Transform.parent = parent;
        }
        
        private int GetOppositeIndex(int i) => i == 0 ? 1 : 0;
    }
}