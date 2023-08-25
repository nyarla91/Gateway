using System;
using Gameplay.Devices;
using UnityEngine;

namespace Gameplay.Surfaces
{
    public class Brick : InputDeviceWatcher
    {
        [SerializeField] private bool _conductive;
        [SerializeField] private BrickView _view;

        public bool Conductive
        {
            get => _conductive;
            private set
            {
                _conductive = value;
                ConductivityChanged?.Invoke(value);
            }
        }

        public event Action<bool> ConductivityChanged; 

        private void OnValidate()
        {
            _view.UpdateMaterial(Conductive);
        }

        protected override void TurnOn() => Conductive = !Conductive;

        protected override void TurnOff() => Conductive = !Conductive;
    }
}