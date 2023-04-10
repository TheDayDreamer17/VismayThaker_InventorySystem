using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    public class GiveDamage : MonoBehaviour
    {
        [SerializeField] private float _damageAmount = 20;
        private void OnCollisionEnter(Collision other)
        {
            IDamagable damagable = other.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.DoDamage(_damageAmount);
            }
        }
    }
}
