using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Autovrse
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class ConsumableItem : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        public ItemData ItemData => _itemData;
        [SerializeField] protected Collider[] _colliders;
        public Collider[] Colliders => _colliders;
        [SerializeField] private MeshRenderer[] _meshRenderers;
        public MeshRenderer[] MeshRenderers => _meshRenderers;

        protected Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        public virtual void OnDropItem(Player player)
        {
            ChangeItemVisibility(true);
            _rb.isKinematic = false;
            _rb.velocity = player.RB.velocity;
            transform.position = player.transform.position;
            _rb.AddForce((player.transform.forward + Util.GetRandomVector3(-1, 1)) * 3, ForceMode.Impulse);
            _rb.AddForce(player.transform.up * 3, ForceMode.Impulse);
            _rb.AddTorque(Util.GetRandomVector3(-1, 1) * 100);
            this.ToggleCollidersArrayAtDelay(_colliders, true);
        }

        public virtual void OnPickItem(Player player)
        {
            ChangeItemVisibility(false);
            _rb.isKinematic = true;
            this.ToggleCollidersArrayAtDelay(_colliders, false);
        }
        // to hide item when picked up  and show when dropped 
        public void ChangeItemVisibility(bool status)
        {
            foreach (var item in _meshRenderers)
            {
                item.enabled = status;
            }
        }

    }
}
