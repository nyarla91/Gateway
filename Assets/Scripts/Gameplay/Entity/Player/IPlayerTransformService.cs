using UnityEngine;

namespace Gameplay.Entity.Player
{
    public interface IPlayerTransformService
    {
        Transform Transform { get; }
    }
}