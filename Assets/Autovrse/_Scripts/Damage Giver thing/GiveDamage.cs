using UnityEngine;
namespace Autovrse
{
    // This gives damage to any I damagable object which touches it 
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
