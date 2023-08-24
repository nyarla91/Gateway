using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Surfaces
{
    public class SurfaceView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material[] _lightMaterials;
        [SerializeField] private Material[] _darkMaterials;

        public void UpdateMaterial(bool canPlaceGateways)
        {
            _renderer.material = canPlaceGateways
                ? _lightMaterials.PickRandomElement()
                : _darkMaterials.PickRandomElement();
        }
    }
}