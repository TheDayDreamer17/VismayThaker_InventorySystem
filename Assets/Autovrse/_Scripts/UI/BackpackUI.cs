using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class BackpackUI : MonoBehaviour
    {
        [SerializeField] private Transform _inventorySlotParent;
        [SerializeField] private InventorySlotUI _inventorySlotPrefab;
        [SerializeField] private List<InventorySlotUI> _inventorySlots = new List<InventorySlotUI>();
        private void OnEnable()
        {
            InventorySystem.OnItemAddedToInventorySystem += OnItemAddedToInventorySystem;
            InventorySystem.OnItemCountModifiedInInventorySystem += OnItemCountModifiedInInventorySystem;
            InventorySystem.OnItemRemovedFromInventorySystem += OnItemRemovedFromInventorySystem;
        }
        private void OnDisable()
        {
            InventorySystem.OnItemAddedToInventorySystem -= OnItemAddedToInventorySystem;
            InventorySystem.OnItemCountModifiedInInventorySystem -= OnItemCountModifiedInInventorySystem;
            InventorySystem.OnItemRemovedFromInventorySystem -= OnItemRemovedFromInventorySystem;
        }

        private void OnItemRemovedFromInventorySystem(InventoryItemData inventoryItemData)
        {
            InventorySlotUI inventorySlotUI = _inventorySlots.Find(InventorySlot => InventorySlot.ID == inventoryItemData.inventoryItem.ItemData.ID);
            if (inventorySlotUI != null)
            {
                _inventorySlots.Remove(inventorySlotUI);
                Destroy(inventorySlotUI.gameObject);
            }
        }

        private void OnItemCountModifiedInInventorySystem(InventoryItemData inventoryItemData)
        {
            InventorySlotUI inventorySlotUI = _inventorySlots.Find(InventorySlot => InventorySlot.ID == inventoryItemData.inventoryItem.ItemData.ID);
            if (inventorySlotUI != null)
            {
                inventorySlotUI.FillSlot(inventoryItemData);
            }
        }

        private void OnItemAddedToInventorySystem(InventoryItemData inventoryItemData)
        {
            InventorySlotUI inventorySlotUI = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, _inventorySlotParent);
            inventorySlotUI.FillSlot(inventoryItemData);
            _inventorySlots.Add(inventorySlotUI);

        }
    }
}
