using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Autovrse
{
    public class InventorySlotDrag_Drop : MonoBehaviour, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Transform image;
        InventorySlotUI inventorySlotUI;

        private void Awake()
        {
            inventorySlotUI = GetComponent<InventorySlotUI>();
        }
        public void OnDrag(PointerEventData eventData)
        {
            image.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.localPosition = Vector3.zero;
        }

        public void OnDrop(PointerEventData eventData)
        {
            RectTransform imageRect = transform.parent as RectTransform;
            if (!RectTransformUtility.RectangleContainsScreenPoint(imageRect, Input.mousePosition))
            {
                GameEvents.NotifyOnItemDroppedFromInventoryUI(inventorySlotUI.CachedInventoryItemData.InventoryItem);
            }
        }
    }
}
