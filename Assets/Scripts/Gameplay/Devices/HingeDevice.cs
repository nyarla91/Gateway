using DG.Tweening;
using UnityEngine;

namespace Gameplay.Devices
{
    public class HingeDevice : InputDeviceWatcher
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _angleOffset;
        [SerializeField] private float _switchDuration;

        private Vector3 _originPosition;
        private Vector3 _targetPosition;
        private Vector3 _originEuler;
        private Vector3 _targetEuler;

        protected override void TurnOn()
        {
            Transform.DOKill();
            Transform.DOLocalMove(_targetPosition, _switchDuration);
            Transform.DOLocalRotate(_targetEuler, _switchDuration);
        }

        protected override void TurnOff()
        {
            Transform.DOKill();
            Transform.DOLocalMove(_originPosition, _switchDuration);
            Transform.DOLocalRotate(_originEuler, _switchDuration);
        }

        protected override void Awake()
        {
            base.Awake();
            
            _originPosition = Transform.localPosition;
            _targetPosition = _originPosition + _positionOffset;
            
            _originEuler = Transform.rotation.eulerAngles;
            _targetEuler = _originEuler + _angleOffset;
        }
        
        
    }
}