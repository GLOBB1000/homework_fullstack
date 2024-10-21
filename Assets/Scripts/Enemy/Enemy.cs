using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public sealed class Enemy : Character, IHealth
    {
        public override event Action<IHealth> OnDeath;

        [SerializeField]
        private float countdown;

        [NonSerialized]
        public Player target;

        private Vector2 destination;
        private float currentTime;
        private bool isPointReached;

        protected override void Start()
        {
            base.Start();
        }

        public void Reset()
        {
            this.currentTime = this.countdown;
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            this.destination = endPoint;
            this.isPointReached = false;
        }

        protected override void FixedUpdate()
        {
            if (this.isPointReached)
            {
                Attack();
            }
            else
            {
                Move((this.destination - (Vector2)this.transform.position));
            }
        }

        public void ChangeHealth(int damage)
        {
            _health -= damage;

            if (_health < 0)
                OnDeath?.Invoke(this);
        }

        public int GetHealth()
        {
            return _health;
        }

        protected override void Move(Vector2 direction)
        {
            if (direction.magnitude <= 0.25f)
            {
                this.isPointReached = true;
                Debug.Log($"Point is reachable {isPointReached}");
                return;
            }

            Vector2 _diretionToPlayer = direction.normalized * Time.fixedDeltaTime;
            Vector2 _nextPosition = _rigidbody.position + direction * _speed;
            _rigidbody.MovePosition(_nextPosition);
        }

        public override void Attack()
        {
            //Attack:
            if (this.target.GetHealth() <= 0)
                return;

            this.currentTime -= Time.fixedDeltaTime;
            if (this.currentTime <= 0)
            {
                Vector2 startPosition = this._firePoint.position;
                Vector2 vector = (Vector2)this.target.transform.position - startPosition;
                Vector2 direction = vector.normalized;

                _bulletManager.SpawnBullet(
                startPosition,
                Color.red,
                (int)PhysicsLayer.ENEMY_BULLET,
                1,
                false,
                direction * 2
            );

                this.currentTime += this.countdown;
            }
        }
    }
}