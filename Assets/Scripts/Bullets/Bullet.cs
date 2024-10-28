using System;
using Intrefaces;
using Ship;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized]
        private ShipHandler _attacker;
        
        [SerializeField]
        private int _damage;

        [SerializeField]
        public new Rigidbody2D rigidbody2D;

        [SerializeField]
        public SpriteRenderer spriteRenderer;

        public void Init(ShipHandler attacker)
        {
            _attacker = attacker;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);

            DealDamage(collision.gameObject);
        }

        private void DealDamage(GameObject other)
        {
            if (_damage <= 0)
                return;

            if (other.TryGetComponent<ShipHandler>(out ShipHandler shipHandler))
            {
                if(shipHandler !=  _attacker)
                    shipHandler.ChangeHealth(_damage);
            }
        }
    }
}