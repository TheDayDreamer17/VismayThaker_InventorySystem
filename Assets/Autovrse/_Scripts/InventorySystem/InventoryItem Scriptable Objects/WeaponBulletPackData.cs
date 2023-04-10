using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "WeaponBulletPackData", menuName = "VismayThaker_InventorySystem/WeaponBulletPackData", order = 0)]
    [System.Serializable]
    public class WeaponBulletPackData : ScriptableObject
    {
        public WeaponData WeaponData;
        public float BulletCount = 5;
    }
}
