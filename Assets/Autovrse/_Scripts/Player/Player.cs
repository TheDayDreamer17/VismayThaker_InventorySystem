using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class Player : MonoBehaviour, IDamagable
    {
        private Rigidbody _rb;
        public Rigidbody RB => _rb;
        private PlayerWeaponController _playerWeaponController;
        public PlayerWeaponController PlayerWeaponController => _playerWeaponController;
        private PlayerMovement _playerMovement;
        public PlayerMovement PlayerMovement => _playerMovement;

        [SerializeField] private float _health = 100;
        public float Heath => _health;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _playerWeaponController = GetComponent<PlayerWeaponController>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public void ModifyHealth(float amount)
        {
            _health = _health + amount > 100 ? 100 : _health + amount;
        }

        public void DoDamage(float damageAmount)
        {
            float newHealth = _health - damageAmount;
            _health = newHealth <= 0 ? 0 : newHealth;
            if (_health == 0)
            {
                //Player is dead
            }
        }
    }
}
