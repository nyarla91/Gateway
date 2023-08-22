using System;
using Extentions;
using UnityEngine;

namespace Gameplay.Entity
{
    public class Rotation : Transformable
    {
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            Transform.Rotate(0, _speed * Time.fixedDeltaTime, 0);
        }
    }
}