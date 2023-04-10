
using UnityEngine;
namespace Autovrse
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "ItemData", menuName = "VismayThaker_InventorySystem/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public string Name;
        public string Description; // show this data on cursor hover
        public Sprite VisualImageInUI;
        public bool CanBeStackable;
    }
}
