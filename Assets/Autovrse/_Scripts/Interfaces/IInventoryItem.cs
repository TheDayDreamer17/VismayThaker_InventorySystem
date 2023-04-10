using UnityEngine;

namespace Autovrse
{
    public interface IInventoryItem
    {
        public ItemData ItemData { get; }

        public Collider[] Colliders { get; }

        void OnPickItem(Player player);
        void OnDropItem(Player player);
    }
}
