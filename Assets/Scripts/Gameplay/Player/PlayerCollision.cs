using System.Collections.Generic;
using System.Linq;
using Extentions;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerCollision : Transformable
    {
        private List<Collision> _currentCollisions = new List<Collision>();

        private Collider _collider;

        private Collider Collider => _collider ??= GetComponent<Collider>();

        public Vector3 Normal { get; private set; }
        public float SlopeAngle => Vector3.Angle(Normal, Vector3.up);
        public bool DoesCollide { get; private set; }

        private void FixedUpdate()
        {
            List<Vector3> normals = new List<Vector3>();
            foreach (Collision collision in _currentCollisions)
            {
                normals.AddRange(collision.contacts.Select(contact => contact.normal));
            }

            if (normals.Count == 0)
            {
                Normal = Vector3.up;
                return;
            }
            Normal = normals.ToArray().AverageVector().normalized;
            DoesCollide = _currentCollisions.Count > 0;
            
            _currentCollisions = new List<Collision>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Transform.parent = other.transform;
        }

        private void OnCollisionExit(Collision other)
        {
            Transform.parent = null;
        }

        private void OnCollisionStay(Collision other)
        {
            _currentCollisions.Add(other);
        }
    }
}