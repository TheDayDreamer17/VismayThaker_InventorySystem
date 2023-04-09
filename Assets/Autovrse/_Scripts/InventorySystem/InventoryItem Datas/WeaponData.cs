using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "WeaponData", menuName = "VismayThaker_InventorySystem/WeaponData", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public int bulletCount;
        public GameObject BulletPrefab;


    }
}
