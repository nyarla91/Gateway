using UnityEngine;

namespace Gameplay.Entity.Player
{
    public interface IPlayerCameraService
    {
        Camera Camera { get; }
        Transform CameraTransform { get; }
    }
}