using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Devices
{
    public abstract class OutputDevice : Transformable
    {
        [SerializeField] private InputDevice _inputDevice;

        protected bool IsTurnedOn => _inputDevice.IsTurnedOn;

        protected abstract void TurnOn();
        protected virtual void TurnOff() { }

        protected virtual void Awake()
        {
            _inputDevice.TurnedOn += TurnOn;
            _inputDevice.TurnedOff += TurnOff;
        }
    }
}