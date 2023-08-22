using Extentions.Factory;
using UnityEngine;
using Zenject;

namespace Gameplay.Gateways
{
    public class GatewaySystem : MonoBehaviour
    {
        [SerializeField] private GameObject[] _gatewaysPrefabs = new GameObject[2];

        [Inject] private ContainerFactory ContainerFactory { get; set; }
        
        private readonly Gateway[] _openGateways = new Gateway[2];

        public void CloseGateway(int index)
        {
            if (_openGateways[index] == null)
                return;
            Destroy(_openGateways[index].gameObject);
        }
        
        public void OpenGateway(int index, Vector3 point, Vector3 normal, Transform surface)
        {
            Vector3 position = point;
            Quaternion rotation = Quaternion.LookRotation(normal, Vector3.up);

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
            currentGateway.Transform.parent = surface;
        }
        
        private int GetOppositeIndex(int i) => i == 0 ? 1 : 0;
    }
}