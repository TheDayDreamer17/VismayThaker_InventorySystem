using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class PlayerConsumableController : MonoBehaviour
    {
        private Player _player;
        public Player Player => _player;
        private void Awake()
        {
            _player = GetComponent<Player>();
        }
        private void OnEnable()
        {
            GameEvents.OnItemConsumed += OnItemConsumed;
        }

        private void OnDisable()
        {
            GameEvents.OnItemConsumed -= OnItemConsumed;
        }

        private void OnItemConsumed(IInventoryConsumableItem inventoryConsumableItem)
        {
            Debug.Log("OnItemConsumed");
            inventoryConsumableItem.OnUseItem(this);
        }
    }
}
