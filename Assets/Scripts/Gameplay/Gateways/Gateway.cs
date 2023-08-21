using System.Collections;
using Extentions;
using Gameplay.Entity.Player;
using UnityEngine;

namespace Gameplay.Gateways
{
    public class Gateway : Transformable
    {
        [SerializeField] private Transform _center;
        [SerializeField] private float _framesCooldown;
        [SerializeField] private Gateway _destination;

        private bool _ready = true;

        private void TeleportHere(Transform target)
        {
            StartCoroutine(StartCooldown());
            target.position = _center.position;
        }

        private IEnumerator StartCooldown()
        {
            _ready = false;
            for (int i = 0; i < _framesCooldown; i++)
            {
                yield return new WaitForFixedUpdate();
            }
            _ready = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if ( ! _ready || ! other.TryGetComponent(out PlayerMovement playerMovement))
                return;
            Transform target = playerMovement.Transform;
            _destination.TeleportHere(target);
        }
    }
}