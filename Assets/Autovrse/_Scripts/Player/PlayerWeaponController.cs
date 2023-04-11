using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class PlayerWeaponController : MonoBehaviour
    {
        private Player _player;
        [SerializeField] private Weapon _currentWeapon = null;
        [SerializeField] private Transform _weaponHolderParent;
        private Transform _mainCamTransform;
        [SerializeField] private float xRotationOffset;
        private void Awake()
        {
            _player = GetComponent<Player>();
        }
        private void Start()
        {
            _mainCamTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            if (_currentWeapon != null)
            {
                _weaponHolderParent.transform.rotation = Quaternion.Euler(new Vector3(_mainCamTransform.eulerAngles.x + xRotationOffset, _weaponHolderParent.eulerAngles.y, _weaponHolderParent.eulerAngles.z));
            }
        }
        public void AttachWeapon(Weapon weapon)
        {
            // make gun parent of player
            weapon.transform.SetParent(_weaponHolderParent);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            _currentWeapon = weapon;
        }

        public void DetachWeapon(Weapon weapon)
        {
            // remove gun from player
            weapon.ResetParent();
            _currentWeapon = null;
        }
        private void OnEnable()
        {
            GameEvents.OnCurrentWeaponDropped += OnCurrentWeaponDropped;
        }
        private void OnDisable()
        {
            GameEvents.OnCurrentWeaponDropped -= OnCurrentWeaponDropped;
        }

        public void DisableCurrentWeapon()
        {
            _currentWeapon?.DisableWeapon();
        }
        public void EnableCurrentWeapon()
        {
            _currentWeapon?.EnableWeapon();
        }
        public void ReloadWeapon()
        {

            _currentWeapon?.ReloadWeapon();
        }

        private void OnCurrentWeaponDropped()
        {
            if (_currentWeapon != null)
                GameEvents.NotifyOnRequestForItemRemovalFromInventory(_player, _currentWeapon as IInventoryItem);
        }

        internal bool MatchWeapon(WeaponData weaponData)
        {
            if (_currentWeapon == null)
                return false;

            return weaponData == _currentWeapon.WeaponData;
        }
    }
}
