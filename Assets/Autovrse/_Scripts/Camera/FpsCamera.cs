using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    public class FpsCamera : MonoBehaviour
    {
        [SerializeField] private float _sensiX = 25, _sensiY = 25;

        [SerializeField] private Transform _playerReference;

        float xRotationValue, yRotationValue;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void OnEnable()
        {
            PlayerInputManager.OnLookDirectionChangeActionFired += OnLookDirectionChangeActionFired;
        }
        private void OnDisable()
        {
            PlayerInputManager.OnLookDirectionChangeActionFired -= OnLookDirectionChangeActionFired;
        }

        private void OnLookDirectionChangeActionFired(Vector2 deltaChange)
        {
            // Get Mouse axis input data
            float mouseXValue = deltaChange.x * Time.deltaTime * _sensiX;
            float mouseYValue = deltaChange.y * Time.deltaTime * _sensiY;

            yRotationValue += mouseXValue;

            xRotationValue -= mouseYValue;

            xRotationValue = Mathf.Clamp(xRotationValue, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotationValue, yRotationValue, 0);
            _playerReference.rotation = Quaternion.Euler(0, yRotationValue, 0);
        }


    }
}
