using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class EnemyShooter : MonoBehaviour
    {
        private bool _isActive = true;
        [SerializeField] private float _attackRange = 5;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private GameObject _projectile;
        private Player _player;
        private void Start()
        {
            StartCoroutine(FindAndShootPlayer());
            _player = GameObject.FindObjectOfType<Player>();
            if (_player != null)
                _isActive = false;
        }

        IEnumerator FindAndShootPlayer()
        {
            while (_isActive)
            {
                if (Physics.CheckSphere(transform.position, _attackRange, _playerLayer))
                {
                    transform.
                    transform.LookAt(_player.transform);
                }
                yield return new WaitForSeconds(2);
            }
            yield return null;
        }
    }
}
