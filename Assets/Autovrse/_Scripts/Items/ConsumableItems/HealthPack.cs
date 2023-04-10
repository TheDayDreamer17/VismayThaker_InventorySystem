using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class HealthPack : ConsumableItem, IInventoryConsumableItem
    {
        public void OnUseItem(PlayerConsumableController playerConsumableController)
        {
            playerConsumableController.Player.ModifyHealth(100);
            Destroy(this.gameObject);
        }

    }
}
