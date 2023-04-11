using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class BulletPacks : ConsumableItem, IInventoryConsumableItem
    {
        [SerializeField] private WeaponBulletPackData _weaponBulletPackData;
        public void OnUseItem(PlayerConsumableController playerConsumableController)
        {
            if (playerConsumableController.Player.PlayerWeaponController.MatchWeapon(_weaponBulletPackData.WeaponData))
            {
                playerConsumableController.Player.PlayerWeaponController.ReloadWeapon();
                Destroy(this.gameObject);
            }
        }
    }
}
