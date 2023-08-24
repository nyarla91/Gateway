using System;
using Gameplay.Gateways;
using UnityEngine;
using Zenject;

namespace Gameplay.Devices
{
    public class GatewayOpenDevice : InputDeviceWatcher
    {
        [SerializeField] private int _gateway;
        
        [Inject] private GatewaySystem GatewaySystem { get; set; }
        
        protected override void TurnOn()
        {
            GatewaySystem.OpenGateway(_gateway, Transform.position, Transform.up, Transform);
        }

        protected override void TurnOff()
        {
            GatewaySystem.CloseGateway(_gateway);
        }

        private void OnValidate()
        {
            _gateway = Mathf.Clamp(_gateway, 0, 1);
        }
    }
}