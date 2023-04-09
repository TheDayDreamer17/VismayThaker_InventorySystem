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
        public static Action<Vector2> OnLookDirectionChangeActionFired;
        private InputAction _movement, _jump, _look;
        public override void Awake()
        {
            base.Awake();
            // Create instance of playercontrols to get input
            if (_playerControls == null)
            {
                _playerControls = new PlayerControls();
                _movement = _playerControls.PlayerMovement.Movement;
                _jump = _playerControls.PlayerMovement.Jump;
                _look = _playerControls.PlayerMovement.Look;
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
            _look.performed += OnLookDirectionChanged;
            _look.Enable();
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
            _look.performed += OnLookDirectionChanged;
            _look.Disable();
        }

        private void OnLookDirectionChanged(InputAction.CallbackContext deltaChange)
        {
            OnLookDirectionChangeActionFired?.Invoke(deltaChange.ReadValue<Vector2>());
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
