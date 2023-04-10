using System;
using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    public static class GameEvents
    {
        // Event invoked by inventory UI
        public static Action<IInventoryItem> OnItemDroppedFromInventoryUI;
        public static void NotifyOnItemDroppedFromInventoryUI(IInventoryItem inventoryItem)
        {
            OnItemDroppedFromInventoryUI?.Invoke(inventoryItem);
        }

        // Event invoked by inventory UI when B pressed 
        public static Action OnInventoryUIStateChanged;
        public static void NotifyOnInventoryUIStateChanged()
        {
            OnInventoryUIStateChanged?.Invoke();
        }

        // Event invoked when G pressed to drop weapon
        public static Action OnCurrentWeaponDropped;
        public static void NotifyOnCurrentWeaponDropped()
        {
            OnCurrentWeaponDropped?.Invoke();
        }

        // Event invoked by player for inventorySystem
        public static Action<Player, IInventoryItem> OnRequestForItemRemovalFromInventory;
        public static void NotifyOnRequestForItemRemovalFromInventory(Player player, IInventoryItem inventoryItem)
        {
            OnRequestForItemRemovalFromInventory?.Invoke(player, inventoryItem);
        }

        // Event invoked by player for inventorySystem
        public static Action<Player, IInventoryItem> OnRequestForAddingItemFromInventory;
        public static void NotifyOnRequestForAddingItemFromInventory(Player player, IInventoryItem inventoryItem)
        {
            OnRequestForAddingItemFromInventory?.Invoke(player, inventoryItem);
        }

        // Event invoked by inventoryData 
        public static Action<InventoryItemData> OnItemAddedToInventorySystem;
        public static void NotifyOnItemAddedToInventorySystem(InventoryItemData inventoryItem)
        {
            OnItemAddedToInventorySystem?.Invoke(inventoryItem);
        }

        // Event invoked by inventoryData 
        public static Action<InventoryItemData> OnItemRemovedFromInventorySystem;
        public static void NotifyOnItemRemovedFromInventorySystem(InventoryItemData inventoryItem)
        {
            OnItemRemovedFromInventorySystem?.Invoke(inventoryItem);
        }

        // Event invoked by inventoryData 
        public static Action<InventoryItemData> OnItemCountModifiedInInventorySystem;
        public static void NotifyOnItemCountModifiedInInventorySystem(InventoryItemData inventoryItem)
        {
            OnItemCountModifiedInInventorySystem?.Invoke(inventoryItem);
        }

        // Event invoked by BackpackUI 
        public static Action<IInventoryConsumableItem> OnItemConsumed;
        public static void NotifyOnItemConsumed(IInventoryConsumableItem inventoryItem)
        {
            OnItemConsumed?.Invoke(inventoryItem);
        }

        // Event for taking health damage and changing ui 
        public static Action<float> OnPlayerHealthChanged;
        public static void NotifyOnPlayerHealthChanged(float newHealth)
        {
            OnPlayerHealthChanged?.Invoke(newHealth);
        }

        // Event for weapon fire
        public static Action OnWeaponFireTriggered;
        public static void NotifyOnWeaponFireTriggered()
        {
            OnWeaponFireTriggered?.Invoke();
        }

        // Event for weapon bullet Request
        public static Action OnWeaponBulletsRequested;
        public static void NotifyOnWeaponBulletsRequested()
        {
            OnWeaponBulletsRequested?.Invoke();
        }
    }
}
// Game ideas
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