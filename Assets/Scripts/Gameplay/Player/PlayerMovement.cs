﻿using System;
using Extentions;
using Extentions.Pause;
using Gameplay.Entity;
using Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerMovement : Transformable, IPlayerTransformService
    {
        private const string RegularState = "Regular";
        
        [SerializeField] private StateMachine _movementStates;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _groundAcceleration;
        [SerializeField] private float _airAcceleration;
        [Space]
        [SerializeField] [Range(0, 90)] private float _walkSlope;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpBufferTime;
        [Space]
        [SerializeField] private float _maxFallSpeed;
        
        private InputBuffer _jumpBuffer;

        private Rigidbody _rigidbody;
        private PlayerCollision _collision;

        private Rigidbody Rigidbody => _rigidbody ??= GetComponent<Rigidbody>();
        private PlayerCollision Collision => _collision ??= GetComponent<PlayerCollision>();

        private bool IsGrounded => Collision.DoesCollide && Collision.SlopeAngle < _walkSlope;

        public event Func<Vector2> MovementInputBind;

        private Vector3 InputDirectionToWorld
            => Transform.forward * MovementInputBind.GetNullableValue().y + Transform.right * MovementInputBind.GetNullableValue().x;
        
        [Inject] private IPauseReadService Pause { get; set; }
        
        private void Move()
        {
            if (Pause.IsPaused)
                return;
            if (_movementStates.IsCurrentStateNoneOf(RegularState))
                return;
            
            Vector3 currentVelocity = Rigidbody.velocity.WithY(0);

            Vector3 direction = InputDirectionToWorld;
            if (Collision.SlopeAngle <= _walkSlope)
                direction = direction - Vector3.Dot(direction, Collision.Normal) * Collision.Normal;
            Vector3 targetVelocity = direction * _maxSpeed;
            
            float maxVelocityDelta = IsGrounded ? _groundAcceleration : _airAcceleration;

            Rigidbody.velocity = Vector3.MoveTowards(currentVelocity, targetVelocity, maxVelocityDelta).WithY(Rigidbody.velocity.y);
        }

        public void TryJump()
        {
            Jump();
            return;
            if (Pause.IsPaused)
                return;
            _jumpBuffer.SendInput();
        }

        private void Jump()
        {
            Rigidbody.velocity = Rigidbody.velocity.WithY(_jumpForce);
        }

        private void Awake()
        {
            _jumpBuffer = new InputBuffer(this, _jumpBufferTime, () => IsGrounded);
            _jumpBuffer.Performed += Jump;
        }

        private void FixedUpdate()
        {
            Move();
            Vector3 velocity = Rigidbody.velocity;
            Rigidbody.velocity = velocity.WithY(Mathf.Max(velocity.y, -_maxFallSpeed));
        }
    }
}