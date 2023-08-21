using System;
using Extentions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputBuffer
    {
        private readonly Timer _timer;
        private float _timeWindow;
        
        public float TimeWindow
        {
            get => _timeWindow;
            set
            {
                _timeWindow = value;
                _timer.Length = value;
            }
        }

        public event Func<bool> PerformCondition;

        public event Action Performed;
        public event Action Expired;

        public InputBuffer(MonoBehaviour container, float timeWindow, Func<bool> performCondition)
        {
            _timeWindow = timeWindow;
            _timer = new Timer(container, timeWindow);
            _timer.Ticked += CheckAvialability;
            _timer.Expired += () => Expired?.Invoke();
            PerformCondition = performCondition;
        }

        public void SendInput(InputAction.CallbackContext context) => SendInput();
        
        public void SendInput()
        {
            _timer.Stop();
            if (PerformCondition.Invoke())
            {
                Performed?.Invoke();
                return;
            }
            _timer.Restart();
        }

        public void InterruptBuffering()
        {
            _timer.Stop();
            Expired?.Invoke();
        }

        private void CheckAvialability(float irrelevant)
        {
            if (!PerformCondition.Invoke())
                return;
            
            Performed?.Invoke();
            _timer.Stop();
        }
    }
}