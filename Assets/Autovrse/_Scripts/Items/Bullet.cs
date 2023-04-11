using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Autovrse
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            _rb.velocity = Vector3.zero;
            this.DoActionWithDelay(() => Destroy(gameObject), 1);
        }

    }
}
