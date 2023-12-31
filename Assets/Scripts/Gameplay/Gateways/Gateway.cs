﻿using System;
using System.Collections;
using Extentions;
using Gameplay.Player;
using UnityEngine;
using Zenject;

namespace Gameplay.Gateways
{
    public class Gateway : Transformable
    {
        [SerializeField] private int _index;
        [SerializeField] private Gateway _destination;
        [SerializeField] private float _framesCooldown;
        [SerializeField] private Transform _center;

        private bool _ready = true;
        protected float _closeRange;
        
        [Inject] private GatewaySystem GatewaySystem { get; set; }
        
        public void Bind(Gateway other)
        {
            _destination = other;
            other._destination = this;
        }

        private void TeleportHere(Transform target)
        {
            StartCoroutine(StartCooldown());
            target.position = _center.position - new Vector3(0, 0.5f, 0);
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

        private void Awake()
        {
            _closeRange = Vector3.Distance(Transform.position, _center.position) - 0.05f;
        }

        private void FixedUpdate()
        {
            LayerMask mask = LayerMask.GetMask("Obstacle");
            if (Physics.OverlapSphere(_center.position, _closeRange, mask).Length == 0)
                return;
            GatewaySystem.CloseGateway(_index);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_destination == null || ! _destination.gameObject.activeSelf || ! _ready || ! other.TryGetComponent(out PlayerMovement playerMovement))
                return;
            Transform target = playerMovement.Transform;
            _destination.TeleportHere(target);
        }
    }
}