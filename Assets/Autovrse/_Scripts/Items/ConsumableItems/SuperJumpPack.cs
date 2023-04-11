using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class SuperJumpPack : ConsumableItem, IInventoryConsumableItem
    {
        [SerializeField] private float _jumpValue = 5, _duration = 5;

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
            Debug.Log("UseItem");
            playerConsumableController.Player.PlayerMovement?.ModifyJumpParameter(_jumpValue, _duration);
            Destroy(this.gameObject);
        }
    }
}
