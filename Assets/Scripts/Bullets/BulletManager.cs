using System.Collections.Generic;
using Common;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance;
        
        [SerializeField]
        private Pool<Bullet> bulletPoolPresenter;
        
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
            this.m_cache.AddRange(bulletPoolPresenter.GetActiveInstances());

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                Bullet bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(Vector2 position, Color color, PhysicsLayer layerMask, Vector2 velocity)
        {
            if (this.bulletPoolPresenter.TryGetInstance(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = bulletPoolPresenter.CreatePoolInstance();
            }

            bullet.SetPosition(position);
            bullet.SetColor(color);
            bullet.SetLayerMask(layerMask);
            bullet.SetVelocity(velocity);

            if (bulletPoolPresenter.TryAddToHashSet(bullet))
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
            foreach (var bullet in bulletPoolPresenter.GetActiveInstances())
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (!this.bulletPoolPresenter.TryRemoveInstance(bullet)) return;
            
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bulletPoolPresenter.ReturnToPool(bullet, bullet.transform);
        }
    }
}