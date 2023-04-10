using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Autovrse
{
    public class InventorySlotUI : MonoBehaviour
    {
        public InventoryItemData CachedInventoryItemData { get; private set; }
        [SerializeField] private Image _itemImageArea;
        [SerializeField] private Image _itemBGArea;
        [SerializeField] private Button _itemBtn;
        public Button ItemBtn => _itemBtn;
        [SerializeField] private TextMeshProUGUI _itemCount;
        private Color _currentColor;
        [SerializeField] private Color _highlightColor;
        private string _name;
        public string Name => _name;
        private void Awake()
        {
            _currentColor = _itemBGArea.color;
        }
        public void FillSlot(InventoryItemData inventoryItemData)
        {
            CachedInventoryItemData = inventoryItemData;
            _name = inventoryItemData.InventoryItem.ItemData.Name;
            _itemImageArea.sprite = inventoryItemData.InventoryItem.ItemData.VisualImageInUI;
            _itemImageArea.enabled = true;
            _itemCount.text = inventoryItemData.Count.ToString();
            _itemCount.enabled = true;
        }

        public void HighlightSlot()
        {
            _itemBGArea.color = _highlightColor;
        }

        public void UnHighlightSlot()
        {
            _itemBGArea.color = _currentColor;
        }


    }
}
