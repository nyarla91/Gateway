using UnityEngine;

namespace Gameplay.Devices
{
    public class SwitchDevice : InputDevice, IInteractable
    {
        [SerializeField] private Transform _buttonOrigin;
        
        public Transform ButtonOrigin => _buttonOrigin;
        
        public void Interact()
        {
            if (IsTurnedOn)
                TurnOff();
            else
                TurnOn();
        }
    }
}