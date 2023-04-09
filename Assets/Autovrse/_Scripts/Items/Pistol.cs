using UnityEngine;
namespace Autovrse
{
    public class Pistol : MonoBehaviour, IInventoryItem, IWeaponItem
    {
        [SerializeField] private ItemData _itemData;
        public ItemData ItemData => _itemData;

        [SerializeField] private WeaponData _weaponData;
        public WeaponData WeaponData => _weaponData;

        public void OnFire()
        {
            Instantiate(_weaponData.BulletPrefab);
        }

        public void OnPickItem(Player player)
        {
            // todo add pistol to player
        }

        public void OnDropItem()
        {
            throw new System.NotImplementedException();
        }
    }

}
