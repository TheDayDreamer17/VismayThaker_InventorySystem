using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Autovrse
{
    public class BackpackUI : MonoBehaviour
    {
        private Canvas _canvas;
        [SerializeField] private Transform _inventorySlotParent;
        [SerializeField] private InventorySlotUI _inventorySlotPrefab;
        [SerializeField] private Button _useButton;
        [SerializeField] private List<InventorySlotUI> _inventorySlots = new List<InventorySlotUI>();
        private InventorySlotUI _currentHighlightedInventorySlotUI = null;
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
            // Hide use button at the start
            _useButton.interactable = false;
        }

        private void OnEnable()
        {
            GameEvents.OnItemAddedToInventorySystem += OnItemAddedToInventorySystem;
            GameEvents.OnItemCountModifiedInInventorySystem += OnItemCountModifiedInInventorySystem;
            GameEvents.OnItemRemovedFromInventorySystem += OnItemRemovedFromInventorySystem;
            GameEvents.OnInventoryUIStateChanged += OnInventoryUIStateChanged;
            _useButton.onClick.AddListener(UseItem);
        }
        private void OnDisable()
        {
            GameEvents.OnItemAddedToInventorySystem -= OnItemAddedToInventorySystem;
            GameEvents.OnItemCountModifiedInInventorySystem -= OnItemCountModifiedInInventorySystem;
            GameEvents.OnItemRemovedFromInventorySystem -= OnItemRemovedFromInventorySystem;
            GameEvents.OnInventoryUIStateChanged -= OnInventoryUIStateChanged;
            _useButton.onClick.RemoveListener(UseItem);
        }

        private void OnInventoryUIStateChanged()
        {
            _canvas.enabled = !_canvas.enabled;
            if (!_canvas.enabled)
            {
                OnBackpackClosed();
            }
        }

        private void OnItemRemovedFromInventorySystem(InventoryItemData inventoryItemData)
        {
            Debug.Log(inventoryItemData.InventoryItem);
            InventorySlotUI inventorySlotUI = _inventorySlots.Find(InventorySlot => InventorySlot.Name == inventoryItemData.InventoryItem.ItemData.Name);
            if (inventorySlotUI != null)
            {
                _inventorySlots.Remove(inventorySlotUI);
                Destroy(inventorySlotUI.gameObject);
            }
        }

        private void OnItemCountModifiedInInventorySystem(InventoryItemData inventoryItemData)
        {
            InventorySlotUI inventorySlotUI = _inventorySlots.Find(InventorySlot => InventorySlot.Name == inventoryItemData.InventoryItem.ItemData.Name);
            if (inventorySlotUI != null)
            {
                inventorySlotUI.FillSlot(inventoryItemData);
            }
        }

        private void OnItemAddedToInventorySystem(InventoryItemData inventoryItemData)
        {
            InventorySlotUI inventorySlotUI = Instantiate(_inventorySlotPrefab, Vector3.zero, Quaternion.identity, _inventorySlotParent);
            inventorySlotUI.FillSlot(inventoryItemData);
            BindToUseBtn(inventorySlotUI);
            _inventorySlots.Add(inventorySlotUI);

        }

        private void BindToUseBtn(InventorySlotUI inventorySlotUI)
        {
            inventorySlotUI.ItemBtn.onClick.AddListener(() => SelectItem(inventorySlotUI));
        }

        private void SelectItem(InventorySlotUI inventorySlotUI)
        {
            if (_currentHighlightedInventorySlotUI != null)
                _currentHighlightedInventorySlotUI.UnHighlightSlot();
            _currentHighlightedInventorySlotUI = inventorySlotUI;
            _currentHighlightedInventorySlotUI.HighlightSlot();

            _useButton.interactable = (_currentHighlightedInventorySlotUI.CachedInventoryItemData.InventoryItem as IInventoryConsumableItem) != null;
        }
        private void UseItem()
        {

            GameEvents.NotifyOnItemConsumed(_currentHighlightedInventorySlotUI.CachedInventoryItemData.InventoryItem as IInventoryConsumableItem);
            GameEvents.NotifyOnItemDroppedFromInventoryUI(_currentHighlightedInventorySlotUI.CachedInventoryItemData.InventoryItem);
            _useButton.interactable = false;
            GameEvents.NotifyOnInventoryUIStateChanged();
        }

        private void OnBackpackClosed()
        {
            if (_currentHighlightedInventorySlotUI != null)
            {
                _currentHighlightedInventorySlotUI.UnHighlightSlot();
                _currentHighlightedInventorySlotUI = null;
            }
            _useButton.interactable = false;
        }
    }
}
