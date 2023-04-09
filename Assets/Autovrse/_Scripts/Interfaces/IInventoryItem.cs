using UnityEngine;
namespace Autovrse
{
    public interface IInventoryItem
    {

        public ItemData ItemData { get; }

        void OnPickItem(Player player);
        void OnDropItem();
    }
}
