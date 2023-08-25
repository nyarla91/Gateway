using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Devices
{
    public abstract class InputDeviceWatcher : Transformable
    {
        [SerializeField] private InputDevice _inputDevice;

        protected bool IsTurnedOn => _inputDevice != null && _inputDevice.IsTurnedOn;

        protected abstract void TurnOn();
        protected virtual void TurnOff() { }

        protected virtual void Awake()
        {
            if (_inputDevice == null)
                return;
            _inputDevice.TurnedOn += TurnOn;
            _inputDevice.TurnedOff += TurnOff;
        }
    }
}