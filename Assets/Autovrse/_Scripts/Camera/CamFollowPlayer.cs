using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _playerReference;

    private void LateUpdate()
    {
        transform.position = _playerReference.position;
    }
}
