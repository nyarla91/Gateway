using System.Collections.Generic;
using System.Linq;
using Extentions;
using UnityEngine;

namespace Gameplay.Entity.Player
{
    public class PlayerCollision : MonoBehaviour
    {
        private List<Collision> _currentCollisions = new List<Collision>();

        private Collider _collider;

        private Collider Collider => _collider ??= GetComponent<Collider>();

        public float SlopeAngle { get; private set; }
        public bool DoesCollide { get; private set; }

        private void Update()
        {
            print(SlopeAngle);
        }

        private void FixedUpdate()
        {
            List<Vector3> normals = new List<Vector3>();
            foreach (Collision collision in _currentCollisions)
            {
                normals.AddRange(collision.contacts.Select(contact => contact.normal));
            }
            float[] angles = normals.Select(normal => Vector3.Angle(normal, Vector3.up)).ToArray();
            
            SlopeAngle = MathExtentions.Average(angles);
            DoesCollide = _currentCollisions.Count > 0;
            
            _currentCollisions = new List<Collision>();
        }

        public void OnCollisionStay(Collision other)
        {
            print(other.gameObject);
            _currentCollisions.Add(other);
        }
    }
}