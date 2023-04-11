using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class BulletPacks : ConsumableItem, IInventoryConsumableItem
    {
        [SerializeField] private WeaponBulletPackData _weaponBulletPackData;
        private void Start()
        {
            OnItemShowCase();
        }

        public override void OnPickItem(Player player)
        {
            base.OnPickItem(player);
            this.StopRotationShowcase();
        }
        public void OnItemShowCase()
        {
            this.DoRotationShowcase(Vector3.up);
        }

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
