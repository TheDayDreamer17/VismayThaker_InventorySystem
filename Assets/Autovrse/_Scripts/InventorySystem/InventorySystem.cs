using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class InventorySystem : Singleton<InventorySystem>
    {
        [SerializeField] private int _backPackSize = 10;
        [SerializeField] private static int BackPackSize => Instance._backPackSize;
        private static List<InventoryItemData> _inventoryItems;
        public static Action<InventoryItemData> OnItemAddedToInventorySystem;
        public static Action<InventoryItemData> OnItemRemovedFromInventorySystem;
        public static Action<InventoryItemData> OnItemCountModifiedInInventorySystem;

        public override void Awake()
        {
            base.Awake();
            _inventoryItems = new List<InventoryItemData>();
        }

        public static void AddItemToInventory(Player player, IInventoryItem inventoryItem)
        {
            if (_inventoryItems.Count >= BackPackSize)
            {

            }
            InventoryItemData inventoryItemData;
            if (!_inventoryItems.Exists(inventoryItemData => inventoryItemData.inventoryItem.ItemData.ID == inventoryItem.ItemData.ID))
            {
                inventoryItemData = new InventoryItemData(inventoryItem);
                _inventoryItems.Add(inventoryItemData);
                OnItemAddedToInventorySystem?.Invoke(inventoryItemData);
            }
            else
            {
                if (inventoryItem.ItemData.CanBeStackable)
                {
                    int index = _inventoryItems.FindIndex(inventoryItemData => inventoryItemData.inventoryItem == inventoryItem);
                    int currentCount = _inventoryItems[index].count;
                    inventoryItemData = new InventoryItemData(inventoryItem, ++currentCount);
                    _inventoryItems[index] = new InventoryItemData(inventoryItem, ++currentCount);
                    OnItemCountModifiedInInventorySystem?.Invoke(inventoryItemData);
                }
            }
            inventoryItem.OnPickItem(player);
        }

        public static void RemoveItemFromInventory(IInventoryItem inventoryItem)
        {
            InventoryItemData inventoryItemData;
            if (_inventoryItems.Exists(inventoryItemData => inventoryItemData.inventoryItem.ItemData.ID == inventoryItem.ItemData.ID))
            {

                inventoryItemData = _inventoryItems.Find(inventoryItemData => inventoryItemData.inventoryItem == inventoryItem);
                _inventoryItems.Remove(inventoryItemData);
                OnItemRemovedFromInventorySystem?.Invoke(inventoryItemData);

            }
        }

    }

    // InventoryItemData containes quantity of inventroy item
    public struct InventoryItemData
    {
        public IInventoryItem inventoryItem;
        public int count;

        public InventoryItemData(IInventoryItem inventoryItem, int count = 1)
        {
            this.inventoryItem = inventoryItem;
            this.count = count;
        }

    }
}
