using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "VismayThaker_InventorySystem/InventoryData", order = 0)]
    public class InventoryData : ScriptableObject
    {
        [SerializeField] private int _backPackSize = 10;
        public List<InventoryItemData> InventoryItems;
        public void Awake()
        {
            InventoryItems = new List<InventoryItemData>();
        }
        public bool IsFull => InventoryItems.Count >= _backPackSize;

        public void AddToInventory(IInventoryItem inventoryItem, Action OnSuccess = null)
        {
            IUniqueInventoryItem uniqueInventoryItem = inventoryItem as IUniqueInventoryItem;
            if ((uniqueInventoryItem) != null
            &&
            InventoryItems.Exists(inventoryItemData => (inventoryItemData.InventoryItem as IUniqueInventoryItem) != null))
            {
                // There exists a unique item already
                return;
            }
            InventoryItemData inventoryItemData;
            if (!InventoryItems.Exists(inventoryItemData => inventoryItemData.InventoryItem.ItemData.Name == inventoryItem.ItemData.Name))
            {
                inventoryItemData = new InventoryItemData(inventoryItem);
                InventoryItems.Add(inventoryItemData);
                GameEvents.NotifyOnItemAddedToInventorySystem(inventoryItemData);
                OnSuccess?.Invoke();
            }
            else
            {
                if (inventoryItem.ItemData.CanBeStackable)
                {
                    ModifyItemInInventory(inventoryItem, increaseQuantity: true, OnSuccess);
                }
            }
        }

        public void ModifyItemInInventory(IInventoryItem inventoryItem, bool increaseQuantity, Action OnSuccess = null)
        {
            int index = InventoryItems.FindIndex(inventoryItemData => inventoryItemData.InventoryItem.ItemData.Name == inventoryItem.ItemData.Name);
            InventoryItems[index].AddAnotherItem(inventoryItem);
            GameEvents.NotifyOnItemCountModifiedInInventorySystem(InventoryItems[index]);
            OnSuccess?.Invoke();
        }


        public void RemoveFromInventory(IInventoryItem inventoryItem, Action OnSuccess = null)
        {
            if (InventoryItems.Exists(inventoryItemData => inventoryItemData.InventoryItem.ItemData.Name == inventoryItem.ItemData.Name))
            {
                int index = InventoryItems.FindIndex(inventoryItemData => inventoryItemData.InventoryItem.ItemData.Name == inventoryItem.ItemData.Name);

                if (InventoryItems[index].Count > 1)
                {
                    InventoryItems[index].RemoveItem(inventoryItem);
                    GameEvents.NotifyOnItemCountModifiedInInventorySystem(InventoryItems[index]);
                }
                else
                {
                    GameEvents.NotifyOnItemRemovedFromInventorySystem(InventoryItems[index]);
                    InventoryItems.Remove(InventoryItems[index]);
                }
                OnSuccess?.Invoke();
            }
        }


    }
}
