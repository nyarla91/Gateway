using DG.Tweening;
using UnityEngine;

namespace Gameplay.Devices.View
{
    public class SwitchDeviceView : InputDeviceWatcher
    {
        [SerializeField] private Transform _joint;
        [SerializeField] private Transform _lever;
        [SerializeField] private Vector3 _rotation;
        [SerializeField] private float _turnDuration;


        protected override void Awake()
        {
            base.Awake();
            _lever.parent = _joint;
        }

        protected override void TurnOn()
        {
            _joint.DOComplete();
            _joint.DORotate( - _rotation, _turnDuration, RotateMode.LocalAxisAdd);
        }

        protected override void TurnOff()
        {
            _joint.DOComplete();
            _joint.DORotate(_rotation, _turnDuration, RotateMode.LocalAxisAdd);
        }
    }
}