using UnityEngine;

namespace Autovrse
{
    // iventory item which are consumable
    public interface IInventoryConsumableItem : IInventoryItem
    {
        public MeshRenderer[] MeshRenderers { get; }
        void OnUseItem(PlayerConsumableController playerConsumableController);
    }
}
