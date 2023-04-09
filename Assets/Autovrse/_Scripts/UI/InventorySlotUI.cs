using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Autovrse
{
    public class InventorySlotUI : MonoBehaviour
    {
        public InventoryItemData cachedInventoryItemData { get; private set; }
        [SerializeField] private Image itemImageArea;
        [SerializeField] private TextMeshProUGUI itemCount;
        private int id = 0;
        public int ID => id;


        public void FillSlot(InventoryItemData inventoryItemData)
        {
            cachedInventoryItemData = inventoryItemData;
            id = inventoryItemData.inventoryItem.ItemData.ID;
            itemImageArea.sprite = inventoryItemData.inventoryItem.ItemData.VisualImageInUI;
            itemImageArea.enabled = true;
            itemCount.text = inventoryItemData.count.ToString();
            itemCount.enabled = true;
        }


    }
}
