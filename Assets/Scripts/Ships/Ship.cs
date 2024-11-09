using System;
using Bullets;
using Common;
using UnityEngine;

namespace Ships
{
    public class Ship : MonoBehaviour
    {
        public event Action OnShipDeath;
        public event Action<Ship> OnDeath;
        
        [SerializeField] private Transform firePoint;
        
        [SerializeField] private Rigidbody2D shipRigidBody;
        
        [SerializeField] private float movementSpeed = 5.0f;

        [SerializeField] private PhysicsLayer bulletPhysicsLayer;
        
        [SerializeField] private float projectileSpeed = 2f;

        private BulletManager bulletManager;
        
        [SerializeField] private Color bulletColor;
        
        [SerializeField] private int maxHealth = 10;
        
        private int health;

        private void Awake()
        {
            bulletManager = BulletManager.Instance;
        }

        private void Start()
        {
            ResetShipInstance();
        }

        public void ResetShipInstance()
        {
            health = maxHealth;
        }
        
        public void ChangeHealth(int damage)
        {
            health -= damage;
            if (health > 0) return;
            
            OnShipDeath?.Invoke();
            OnDeath?.Invoke(this);
        }

        public int GetHealth()
        {
            return health;
        }
        
        private void Attack(Vector2 velocity)
        {
            bulletManager.SpawnBullet(firePoint.position, bulletColor, bulletPhysicsLayer, velocity);
        }

        public void Attack()
        {
            Vector2 velocity = firePoint.rotation * Vector3.up * projectileSpeed;

            Attack(velocity);
        }

        public void Attack(Transform target)
        {
            Vector2 velocity = ((Vector2)target.transform.position - (Vector2)firePoint.position).normalized * projectileSpeed;
            
            Attack(velocity);
        }
        
        public void Move(Vector2 direction, float deltaTime)
        {
            Vector2 moveStep = direction * movementSpeed * deltaTime;
            Vector2 targetPosition = shipRigidBody.position + moveStep;
            shipRigidBody.MovePosition(targetPosition);
        }
    }
}