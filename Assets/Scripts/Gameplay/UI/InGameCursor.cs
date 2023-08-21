using System;
using Extentions;
using Extentions.Pause;
using UnityEngine;
using Zenject;

namespace Gameplay.UI
{
    public class InGameCursor : MonoBehaviour
    {
        [Inject] private IPauseReadService Pause { get; set; }
        
        private void Start()
        {
            Cursor.lockState = Pause.IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}