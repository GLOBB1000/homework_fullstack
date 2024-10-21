using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance;

        [SerializeField]
        public Bullet prefab;

        [SerializeField]
        public Transform worldTransform;

        [SerializeField]
        private LevelBounds levelBounds;
        
        [SerializeField]
        private Transform container;

        public readonly HashSet<Bullet> m_activeBullets = new();
        public readonly Queue<Bullet> m_bulletPool = new();
        private readonly List<Bullet> m_cache = new();

        private void Awake()
        {
            if(Instance == null)
                Instance = this;

            for (var i = 0; i < 10; i++)
            {
                Bullet bullet = Instantiate(this.prefab, this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            this.m_cache.Clear();
            this.m_cache.AddRange(this.m_activeBullets);

            for (int i = 0, count = this.m_cache.Count; i < count; i++)
            {
                Bullet bullet = this.m_cache[i];
                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }

        public void SpawnBullet(Vector2 position, Color color, IHealth attacker, Vector2 velocity)
        {
            if (this.m_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldTransform);
            }
            else
            {
                bullet = Instantiate(this.prefab, this.worldTransform);
            }

            bullet.Init(attacker);

            bullet.transform.position = position;
            bullet.spriteRenderer.color = color;
            bullet.rigidbody2D.velocity = velocity;
            bullet.gameObject.layer = attacker.GetType() == typeof(Player) ? (int)PhysicsLayer.PLAYER_BULLET : (int)PhysicsLayer.ENEMY_BULLET;

            if (m_activeBullets.Add(bullet))
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
            foreach (var bullet in m_activeBullets)
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
            }
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (this.m_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= this.OnBulletCollision;
                bullet.transform.SetParent(this.container);
                this.m_bulletPool.Enqueue(bullet);
            }
        }
    }
}