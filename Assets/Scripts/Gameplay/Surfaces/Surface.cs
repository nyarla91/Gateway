using System;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Surfaces
{
    public class Surface : MonoBehaviour
    {
        [SerializeField] private bool _canPlaceGateways;
        [Space]
        [SerializeField] private SurfaceView _view;
        [Space]
        [SerializeField] protected GameObject _surfacePreafb;
        [SerializeField] protected float _gridSize;
        [SerializeField] protected bool _push;
        [SerializeField] protected bool _pull;

        public bool CanPlaceGateways => _canPlaceGateways;

        private void OnValidate()
        {
            _view.UpdateMaterial(_canPlaceGateways);
        }
    }
}