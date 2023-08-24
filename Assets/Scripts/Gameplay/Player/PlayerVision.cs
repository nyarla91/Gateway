using System;
using System.Collections;
using Extentions;
using Extentions.Pause;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerVision : Transformable, IPlayerCameraService
    {
        [SerializeField] private Camera _camera;

        private Coroutine _turnAroundCoroutine;
        private Transform _cameraTransform;
        private bool _aiming;
        
        public Camera Camera => _camera;
        public Transform CameraTransform => _cameraTransform;
        
        public Ray LookRay => new Ray(CameraTransform.position, CameraTransform.forward);

        public event Func<Vector2> CameraRotationBind;

        [Inject] private IPauseReadService Pause { get; set; }

        private void RotateCamera(Vector2 delta)
        {
            if (Pause.IsPaused)
                return;
            float verticalAngle = _cameraTransform.localRotation.eulerAngles.x;
            verticalAngle -= delta.y;
            verticalAngle = verticalAngle.ClampAngle(271, 89);
            _cameraTransform.localRotation = Quaternion.Euler(_cameraTransform.localRotation.eulerAngles.WithX(verticalAngle));
            Transform.Rotate(0, delta.x, 0);
        }

        private void Awake()
        {
            _cameraTransform = _camera.transform;
        }

        private void Update()
        {
            RotateCamera(CameraRotationBind.GetNullableValue() * Time.deltaTime);
        }
    }

    public class VisionRaycast
    {
        public bool Hit { get; }
        public RaycastHit Raycast { get; }
        public int Layer { get; }

        public VisionRaycast(RaycastHit raycast)
        {
            Raycast = raycast;
            Hit = raycast.collider != null;
            if (Hit)
                Layer = raycast.collider.gameObject.layer;
        }
    }
}