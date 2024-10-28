using System;
using Intrefaces;
using ShootEmUp;
using UnityEngine;

namespace Ship
{
    public class ShipHandler : MonoBehaviour
    {
        public event Action OnShipDeath;
        public event Action<ShipHandler> OnShipDeathVariable;
        
        [SerializeField]
        private Transform _firePoint;
        
        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        [SerializeField]
        private float _speed = 5.0f;

        private BulletManager _bulletManager;
        
        [SerializeField]
        private int _maxHealth = 10;
        
        private int _health;

        private void Awake()
        {
            _bulletManager = BulletManager.Instance;
        }

        public void ResetShipInstance()
        {
            _health = _maxHealth;
        }
        
        public void ChangeHealth(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                OnShipDeath?.Invoke();
                OnShipDeathVariable?.Invoke(this);
            }
        }

        public int GetHealth()
        {
            return _health;
        }

        public void Attack(Color color, Transform target = null)
        {
            Vector2 velocity = target == null ? 
                _firePoint.rotation * Vector3.up * 3 : 
                ((Vector2)target.transform.position - (Vector2)_firePoint.position).normalized * 2;
            
            _bulletManager.SpawnBullet(_firePoint.position, color, this, velocity);
        }

        public void Move(Vector2 direction, float deltaTime)
        {
            Vector2 moveStep = direction * _speed * deltaTime;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }
    }
}