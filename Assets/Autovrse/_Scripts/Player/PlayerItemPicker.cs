using System;
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
        private void OnEnable()
        {
            GameEvents.OnItemDroppedFromInventoryUI += OnItemDroppedFromInventory;
        }
        private void OnDisable()
        {
            GameEvents.OnItemDroppedFromInventoryUI -= OnItemDroppedFromInventory;
        }

        private void OnItemDroppedFromInventory(IInventoryItem inventoryItem)
        {
            GameEvents.NotifyOnRequestForItemRemovalFromInventory(_player, inventoryItem);
        }

        private void OnCollisionEnter(Collision other)
        {

            // Check if parent has IInventoryItem
            IInventoryItem inventoryItem = other.collider.GetComponentInParent<IInventoryItem>();
            if (inventoryItem != null)
            {
                GameEvents.NotifyOnRequestForAddingItemFromInventory(_player, inventoryItem);
            }
        }
    }
}
