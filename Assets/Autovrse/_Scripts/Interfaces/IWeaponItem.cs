using UnityEngine;
namespace Autovrse
{
    public interface IWeaponItem
    {
        WeaponData WeaponData { get; }
        void OnFire();
    }
}
