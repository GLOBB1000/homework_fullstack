using System.Collections.Generic;
using Intrefaces;
using Ship;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance;
        
        [SerializeField]
        private BulletPoolPresenter bulletPoolPresenter;
        
        [SerializeField]
        private LevelBounds levelBounds;
        
        [SerializeField]
        private Transform worldTransform;

        private List<Bullet> m_cache = new List<Bullet>();
        
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
        }

        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(bulletPoolPresenter.BulletPool.GetActiveInstances());

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                Bullet bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    Debug.Log("Remove bullet");
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(Vector2 position, Color color, ShipHandler attacker, Vector2 velocity)
        {
            if (this.bulletPoolPresenter.BulletPool.TryGetInstance(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = bulletPoolPresenter.BulletPool.CreatePoolInstance();
            }

            bullet.Init(attacker);

            bullet.transform.position = position;
            bullet.spriteRenderer.color = color;
            bullet.rigidbody2D.velocity = velocity;
            bullet.gameObject.layer = attacker.GetComponent<Player>() != null ? (int)PhysicsLayer.PLAYER_BULLET : (int)PhysicsLayer.ENEMY_BULLET;

            if (bulletPoolPresenter.BulletPool.TryAddToHashSet(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            this.RemoveBullet(bullet);
        }

        private void OnDestroy()
        {
            foreach (var bullet in bulletPoolPresenter.BulletPool.GetActiveInstances())
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            Debug.Log("Remove bullet"); 
            if (!this.bulletPoolPresenter.BulletPool.TryRemoveInstance(bullet)) return;
            
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bulletPoolPresenter.BulletPool.ReturnToPool(bullet, bullet.transform);
        }
    }
}