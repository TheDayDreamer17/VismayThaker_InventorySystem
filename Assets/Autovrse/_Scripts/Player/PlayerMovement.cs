using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class PlayerMovement : MonoBehaviour
    {
        Rigidbody _rb;
        [SerializeField] private float _upwardForce = 5;
        [SerializeField] private float _movementSpeed = 5;

        [SerializeField] private float _rotationSpeed = 10;
        private bool isInAir = false;
        private Vector3 _moveDirection;

        private Vector2 _movementValue;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            // Subscribe to input managet events
            PlayerInputManager.OnMovementActionFired += OnMovementActionFired;
            PlayerInputManager.OnJumpActionFired += OnJumpActionFired;
        }
        private void OnDisable()
        {
            // UnSubscribe to input managet events
            PlayerInputManager.OnMovementActionFired -= OnMovementActionFired;
            PlayerInputManager.OnJumpActionFired -= OnJumpActionFired;
        }

        private void FixedUpdate()
        {
            CalculateMovement();
        }

        private void CalculateMovement()
        {
            // Changing position and rotation based on input
            _moveDirection = transform.forward * _movementValue.y;
            _moveDirection += transform.right * _movementValue.x;
            _moveDirection.Normalize();
            // _moveDirection.y = 0;
            Vector3 targetDirection = _moveDirection;
            _rb.velocity = _moveDirection * _movementSpeed;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion finalRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            transform.rotation = finalRotation;
        }

        private void OnMovementActionFired(Vector2 movementData)
        {
            _movementValue = movementData;
            // _rb.AddForce(transform.position + new Vector3(movementData.x, 0, movementData.y));
        }

        private void OnJumpActionFired()
        {
            isInAir = true;
            _rb.AddForce(transform.up * _upwardForce, ForceMode.Impulse);
        }

    }
}
