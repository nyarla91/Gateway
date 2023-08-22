using System;
using DG.Tweening;
using Extentions;
using UnityEngine;

namespace Gameplay.UI
{
    public class InteractablePrompt : Transformable
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _animationDuration;
        
        private Watch<bool> _targetExists;



        public event Func<IInteractable, Vector2> InteractableScreenPosition; 
        public event Func<IInteractable> TargetBind;

        private void Awake()
        {
            _targetExists.OnChanged += ToggleShow;
        }

        private void ToggleShow(bool _, bool targetExists)
        {
            RectTransform.DOKill();
            RectTransform.DOScale(targetExists ? Vector3.one : (Vector3.one * 2), _animationDuration);
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(targetExists ? 1 : 0, _animationDuration);
        }

        private void FixedUpdate()
        {
            IInteractable target = TargetBind.GetNullableValue();
            _targetExists.Value = TargetBind.GetNullableValue() != null;
            if (_targetExists)
                RectTransform.anchoredPosition = InteractableScreenPosition(target);
        }
    }
}