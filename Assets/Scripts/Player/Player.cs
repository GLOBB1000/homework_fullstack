using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Player : Character, IHealth
    {
        public override event Action<IHealth> OnDeath;

        private float _moveDirection;
        
        public float MoveDirection => _moveDirection;

        protected override void Start()
        {
            base.Start();
        }

        public void ChangeHealth(int _damage)
        {
            _health -= _damage;

            if (_health < 0)
                OnDeath?.Invoke(this);
        }

        public int GetHealth()
        {
            return _health;
        }

        public override void Attack()
        {
            _bulletManager.SpawnBullet(_firePoint.position, Color.blue, this, _firePoint.rotation * Vector3.up * 3);
        }

        protected override void FixedUpdate()
        {
            Move(new Vector2(_moveDirection, 0));
        }

        protected override void Move(Vector2 direction)
        {
            Vector2 moveStep = direction * Time.fixedDeltaTime * _speed;
            Vector2 targetPosition = _rigidbody.position + moveStep;
            _rigidbody.MovePosition(targetPosition);
        }

        public void SetDirection(float direction)
        {
            _moveDirection = direction;
        }
    }
}