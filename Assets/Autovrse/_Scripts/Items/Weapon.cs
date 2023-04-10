using System;
using UnityEngine;
namespace Autovrse
{
    [RequireComponent(typeof(Rigidbody))]
    public class Weapon : MonoBehaviour, IUniqueInventoryItem, IWeaponItem
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private ItemData _itemData;
        public ItemData ItemData => _itemData;
        [SerializeField] private WeaponData _weaponData;
        public WeaponData WeaponData => _weaponData;
        [SerializeField] private Collider[] _colliders;
        public Collider[] Colliders => _colliders;

        private Rigidbody _rb;
        private Transform _originalParent;

        private void Awake()
        {
            _originalParent = transform.parent;
            _rb = GetComponent<Rigidbody>();
        }
        public void OnFire()
        {
            GameObject bullet = Instantiate(_weaponData.BulletPrefab, _shootPoint.transform.position, _shootPoint.transform.rotation);
            Vector3 direction = _shootPoint.forward;
            bullet.GetComponent<Rigidbody>().AddForce(direction * 30, ForceMode.Impulse);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                IDamagable damagable = hit.collider.GetComponent<IDamagable>();
                damagable?.DoDamage(_weaponData.DamageAmount);
            }
        }
        public void ReloadWeapon()
        {

        }
        private void OnMagazineEmpty()
        {
            GameEvents.NotifyOnWeaponBulletsRequested();
        }

        public void OnPickItem(Player player)
        {
            GameEvents.OnWeaponFireTriggered += OnFire;
            Debug.Log("OnPickItem");
            player.PlayerWeaponController?.AttachWeapon(this);
            _rb.isKinematic = true;
            this.ToggleCollidersArrayAtDelay(_colliders, false);
        }

        public void OnDropItem(Player player)
        {
            GameEvents.OnWeaponFireTriggered -= OnFire;
            player.PlayerWeaponController?.DetachWeapon(this);

            // Add force to show item dropping from inventory
            _rb.isKinematic = false;
            _rb.velocity = player.RB.velocity;
            _rb.AddForce(player.transform.forward * _weaponData.DropFrontForce, ForceMode.Impulse);
            _rb.AddForce(player.transform.up * _weaponData.DropUpwardForce, ForceMode.Impulse);
            _rb.AddTorque(Util.GetRandomVector3(-1, 1) * _weaponData.DropTorqueForce);

            this.ToggleCollidersArrayAtDelay(_colliders, true, 0.5f);

        }

        // Reset parent when dropped
        public void ResetParent()
        {
            transform.SetParent(_originalParent);
        }


    }

}
