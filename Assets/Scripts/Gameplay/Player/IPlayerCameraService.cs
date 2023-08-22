using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerCameraService
    {
        Camera Camera { get; }
        Transform CameraTransform { get; }
    }
}