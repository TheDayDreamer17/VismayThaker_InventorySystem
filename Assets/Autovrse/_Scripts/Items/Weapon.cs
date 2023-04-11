using System;
using System.Collections;
using UnityEngine;
using TMPro;
namespace Autovrse
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(AudioSource))]
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
        private int _bulletsLeft, _bulletsShot;
        Coroutine _weaponFireCoroutine = null;
        RaycastHit _hit;
        private bool _readyToShoot = true, _reloading = false, _canShoot = true;
        [SerializeField] private TMP_Text _bulletCountText;
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _originalParent = transform.parent;
            _rb = GetComponent<Rigidbody>();
            _bulletsLeft = _weaponData.MagazineSize;
        }
        IEnumerator OnFireCoroutine()
        {
            do
            {
                if (_bulletsLeft == 0)
                {
                    OnMagazineEmpty();
                    yield break;
                }
                if (!_canShoot)
                    yield break;
                if (_readyToShoot && !_reloading)
                    Shoot();
                yield return new WaitForSeconds(_weaponData.TimeBetweenShooting);
                _readyToShoot = true;
            } while (_weaponData.AllowButtonHold);
            _weaponFireCoroutine = null;
            yield return null;
        }
        private void Shoot()
        {
            _readyToShoot = false;
            _bulletsLeft--;
            _bulletCountText.text = _bulletsLeft.ToString();
            _audioSource.Play();
            GameObject bullet = Instantiate(_weaponData.BulletPrefab, _shootPoint.transform.position, _shootPoint.transform.rotation);
            Vector3 direction = _shootPoint.forward;
            bullet.GetComponent<Rigidbody>().AddForce(direction * 10 * _weaponData.Range, ForceMode.Impulse);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out _hit))
            {
                IDamagable damagable = _hit.collider.GetComponent<IDamagable>();
                damagable?.DoDamage(_weaponData.DamageAmount);
            }
        }
        public void OnFireStart()
        {
            if (_bulletsLeft == 0)
            {
                OnMagazineEmpty();
                return;
            }
            if (!_canShoot) return;
            _audioSource.loop = _weaponData.AllowButtonHold;
            _readyToShoot = true;
            _weaponFireCoroutine = StartCoroutine(OnFireCoroutine());
        }
        public void OnFireStop()
        {
            _audioSource.Stop();
            if (_weaponFireCoroutine != null)
                StopCoroutine(_weaponFireCoroutine);
            _weaponFireCoroutine = null;
        }
        public void ReloadWeapon()
        {
            _reloading = true;
            this.DoActionWithDelay(() =>
            {
                _bulletsLeft += _weaponData.MagazineSize;
                _bulletCountText.text = _bulletsLeft.ToString();
                _reloading = false;
            }, _weaponData.ReloadTime);
        }
        private void OnMagazineEmpty()
        {
            OnFireStop();
        }

        public void OnPickItem(Player player)
        {
            // Depending upon the gun type subscribe shooting mode
            GameEvents.OnWeaponFireTriggered += OnFireStart;
            GameEvents.OnWeaponFireStopped += OnFireStop;

            player.PlayerWeaponController?.AttachWeapon(this);
            _rb.isKinematic = true;
            this.ToggleCollidersArrayAtDelay(_colliders, false);
        }

        public void OnDropItem(Player player)
        {
            // unsubscribe shooting mode on drop
            GameEvents.OnWeaponFireTriggered -= OnFireStart;
            GameEvents.OnWeaponFireStopped -= OnFireStop;

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

        internal void DisableWeapon()
        {
            _canShoot = false;
        }

        internal void EnableWeapon()
        {
            _canShoot = true;
        }
    }

}
