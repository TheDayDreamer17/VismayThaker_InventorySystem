using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    [System.Serializable]
    // InventoryItemData containes quantity of inventory item
    public struct InventoryItemData
    {
        [SerializeField] private List<IInventoryItem> _inventoryItemList;
        public IInventoryItem InventoryItem => _inventoryItemList[_inventoryItemList.Count - 1];
        public int Count => _inventoryItemList.Count;

        public InventoryItemData(IInventoryItem inventoryItem)
        {
            this._inventoryItemList = new List<IInventoryItem>();
            this._inventoryItemList.Add(inventoryItem);
        }

        public void AddAnotherItem(IInventoryItem inventoryItem)
        {
            if (!this._inventoryItemList.Contains(inventoryItem))
                this._inventoryItemList.Add(inventoryItem);
        }
        public void RemoveItem(IInventoryItem inventoryItem)
        {
            if (this._inventoryItemList.Contains(inventoryItem))
                this._inventoryItemList.Remove(inventoryItem);
        }

    }
}
