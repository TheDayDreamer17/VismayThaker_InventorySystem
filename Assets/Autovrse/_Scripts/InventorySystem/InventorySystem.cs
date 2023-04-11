using System;
using UnityEngine;
namespace Autovrse
{
    public class InventorySystem : MonoBehaviour
    {
        // Reference to the inventory data scriptable object
        [SerializeField] private InventoryData _inventory;
        private void Awake()
        {
            _inventory.InventoryItems.Clear();
        }
        private void OnEnable()
        {
            // Subscribing events made by player
            GameEvents.OnRequestForAddingItemFromInventory += AddItemToInventory;
            GameEvents.OnRequestForItemRemovalFromInventory += RemoveItemFromInventory;
        }
        private void OnDisable()
        {
            // UnSubscribing events made by player
            GameEvents.OnRequestForAddingItemFromInventory -= AddItemToInventory;
            GameEvents.OnRequestForItemRemovalFromInventory -= RemoveItemFromInventory;
        }

        public void AddItemToInventory(Player player, IInventoryItem inventoryItem)
        {
            if (_inventory == null)
            {
                Debug.Log("Assign inventory Data, inventory Data is null");
                return;
            }
            if (_inventory.IsFull)
            {
                Debug.Log("bag is full");
                return;
            }
            _inventory.AddToInventory(inventoryItem, () => inventoryItem.OnPickItem(player));

        }

        public void RemoveItemFromInventory(Player player, IInventoryItem inventoryItem)
        {
            if (_inventory == null)
            {
                Debug.Log("Inventory Data is null");
                return;
            }

            _inventory.RemoveFromInventory(inventoryItem, () => inventoryItem.OnDropItem(player));


        }

    }
}
