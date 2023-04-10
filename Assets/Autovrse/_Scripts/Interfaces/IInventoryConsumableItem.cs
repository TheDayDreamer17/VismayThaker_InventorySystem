using UnityEngine;

namespace Autovrse
{
    public interface IInventoryConsumableItem : IInventoryItem
    {
        public MeshRenderer[] MeshRenderers { get; }
        void OnUseItem(PlayerConsumableController playerConsumableController);
    }
}
