using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Autovrse
{
    public class PlayerInputManager : Singleton<PlayerInputManager>
    {
        private PlayerControls _playerControls;

        // Defining Movement Events For player
        public static Action<Vector2> OnMovementActionFired;
        public static Action OnJumpActionFired;
        private InputAction _movement, _jump;
        public override void Awake()
        {
            base.Awake();
            // Create instance of playercontrols to get input
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();
                _movement = _playerControls.PlayerMovement.Movement;
                _jump = _playerControls.PlayerMovement.Jump;
            }
        }
        private void OnEnable()
        {
            if (_playerControls == null)
                return;
            // Enable player actions and bind events on action performed 
            _movement.performed += OnMovementPerformed;
            _movement.Enable();
            _jump.performed += OnJumpPerformed;
            _jump.Enable();
        }

        private void OnDisable()
        {
            if (_playerControls == null)
                return;
            // Disable player actions and unbind events on action performed
            _movement.performed -= OnMovementPerformed;
            _movement.Disable();
            _jump.performed -= OnJumpPerformed;
            _jump.Disable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext moveData)
        {
            OnMovementActionFired?.Invoke(moveData.ReadValue<Vector2>());
        }

        private void OnJumpPerformed(InputAction.CallbackContext jumpData)
        {
            if (jumpData.ReadValueAsButton())
                OnJumpActionFired?.Invoke();
        }
    }
}
