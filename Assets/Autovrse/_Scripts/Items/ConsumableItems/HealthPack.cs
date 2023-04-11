using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class HealthPack : ConsumableItem, IInventoryConsumableItem
    {
        private void Start()
        {
            OnItemShowCase();
        }
        public void OnItemShowCase()
        {
            this.DoRotationShowcase(Vector3.up);
        }
        public override void OnPickItem(Player player)
        {
            base.OnPickItem(player);
            this.StopRotationShowcase();
        }
        public void OnUseItem(PlayerConsumableController playerConsumableController)
        {
            playerConsumableController.Player.ModifyHealth(100);
            Destroy(this.gameObject);
        }

    }
}
