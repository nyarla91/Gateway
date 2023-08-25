using System;
using Extentions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Surfaces
{
    public class BrickView : MonoBehaviour
    {
        [SerializeField] protected Brick _brick;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Material _conductiveMaterial;
        [SerializeField] private Material _resistantMaterial;
        [SerializeField] private float _hueAbberation;
        [SerializeField] private float _saturationAbberation;
        [SerializeField] private float _valueAbberation;

        private float _valueOffset;
        private float _hueOffset;
        private float _saturationOffset;
        
        private void Awake()
        {
            _hueOffset = Random.Range(-_hueAbberation, _hueAbberation);
            _saturationOffset = Random.Range(-_saturationAbberation, _saturationAbberation);
            _valueOffset = Random.Range(-_valueAbberation, _valueAbberation);
            _brick.ConductivityChanged += UpdateMaterial;
            UpdateMaterial(_brick.Conductive);
        }

        public void UpdateMaterial(bool conductivity)
        {
            if (_renderer == null)
                return;
            _renderer.material = conductivity ? _conductiveMaterial : _resistantMaterial;
            if (_saturationOffset.Equals(0) || _hueOffset.Equals(0) || _valueOffset.Equals(0))
                return;
            Color.RGBToHSV(_renderer.material.color, out float h, out float s, out float v);
            h += _hueOffset;
            s += _saturationAbberation;
            v += _valueOffset;
            _renderer.material.color = Color.HSVToRGB(h, s, v);
        }
    }
}