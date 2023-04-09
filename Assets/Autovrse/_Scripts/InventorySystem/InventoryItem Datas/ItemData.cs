
using UnityEngine;
namespace Autovrse
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ItemData", menuName = "VismayThaker_InventorySystem/ItemData", order = 0)]
    public class ItemData : ScriptableObject
    {
        public int ID;
        public string Name;
        public string Description;
        public Sprite VisualImageInUI;
        public bool CanBeStackable;


    }
}
