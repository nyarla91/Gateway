using UnityEngine;

namespace Gameplay
{
    public interface IInteractable
    {
        void Interact();
        Transform ButtonOrigin { get; }
    }
}