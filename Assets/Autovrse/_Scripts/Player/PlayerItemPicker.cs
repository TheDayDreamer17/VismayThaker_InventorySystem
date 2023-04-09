using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class PlayerItemPicker : MonoBehaviour
    {
        private Player _player;
        private void Awake()
        {
            _player = GetComponent<Player>();
        }
        private void OnCollisionEnter(Collision other)
        {
            IInventoryItem inventoryItem = other.collider.GetComponent<IInventoryItem>();
            if (inventoryItem != null)
            {
                InventorySystem.AddItemToInventory(_player, inventoryItem);
            }
        }
    }
}
