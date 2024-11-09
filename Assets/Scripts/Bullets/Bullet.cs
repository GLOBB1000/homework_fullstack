using System;
using Common;
using Ships;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [SerializeField]
        private int _damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            this.OnCollisionEntered?.Invoke(this, collision);

            DealDamage(collision.gameObject);
        }

        private void DealDamage(GameObject other)
        {
            if (_damage <= 0)
                return;

            if (other.TryGetComponent<Ship>(out Ship shipHandler))
            {
                shipHandler.ChangeHealth(_damage);
                    
            }
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetLayerMask(PhysicsLayer layerMask)
        {
            gameObject.layer = (int)layerMask;
        }
    }
}