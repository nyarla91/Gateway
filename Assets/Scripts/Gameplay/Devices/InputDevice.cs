using System;
using UnityEngine;

namespace Gameplay.Devices
{
    public class InputDevice : MonoBehaviour
    {
        public bool IsTurnedOn { get; private set; }

        public event Action TurnedOn;
        public event Action TurnedOff;

        protected void TurnOn()
        {
            IsTurnedOn = true;
            TurnedOn?.Invoke();
        }
        
        protected void TurnOff()
        {
            IsTurnedOn = false;
            TurnedOff?.Invoke();
        }
    }
}