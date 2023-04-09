using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{

    public abstract class InventoryItem : ScriptableObject
    {
        [SerializeField] protected ItemData _itemData;
        protected itemType _itemType;
        public ItemData ItemData => _itemData;
        public itemType ItemType => _itemType;
        protected bool canBeStackable = true;
        public bool CanBeStackable => canBeStackable;
        protected Sprite viewInUI;
        protected virtual void OnItemPicked()
        {
            Debug.Log("Picked Item" + _itemData.Name);
        }
        protected virtual void OnItemDropped()
        {
            Debug.Log("Dropped Item" + _itemData.Name);
        }

    }

    public enum itemType
    {
        Stackable, NonConsumable
    }
}

// Health bar
// super jump
// invisibility
//consumables

// non consumables
// helmet
// shield
// gun
// bullets
// bomb