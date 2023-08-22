using System.Linq;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] protected float _radius;
        
        private PlayerVision _vision;

        public PlayerVision Vision => _vision ??= GetComponent<PlayerVision>();
        
        public IInteractable Target { get; private set; }

        public void TryInteract()
        {
            Target?.Interact();
        }
        
        private void FixedUpdate()
        {
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            Collider[] colliders = Physics.OverlapSphere(Vision.CameraTransform.position, _radius);
            IInteractable[] possibleTargets = colliders
                .Select(collider => collider.GetComponent<IInteractable>())
                .Where(target => target != null)
                .Where(target => IsPointOnScreen(InteractableScreenPosition(target)))
                .ToArray();
            
            if (possibleTargets.Length == 0)
            {
                Target = null;
                return;
            }
            if (possibleTargets.Length == 1)
            {
                Target = possibleTargets[0];
                return;
            }

            possibleTargets = possibleTargets.OrderBy(target
                => Vector2.Distance(new Vector2(Screen.width / 2, Screen.height / 2),
                    InteractableScreenPosition(target))).ToArray();

            Target = possibleTargets[0];
        }

        private bool IsPointOnScreen(Vector2 point) =>
            point.x < Screen.width && point.x > 0 && point.y < Screen.height && point.y > 0;

        public Vector2 InteractableScreenPosition(IInteractable obj) =>
            Vision.Camera.WorldToScreenPoint(obj.ButtonOrigin.position);
    }
}