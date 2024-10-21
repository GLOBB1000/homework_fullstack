using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Enemy : Character, IHealth
    {
        public override event Action<IHealth> OnDeath;

        [SerializeField]
        private float _countdown;

        [NonSerialized]
        public Player _target;

        private Vector2 _destination;
        private float _currentTime;
        private bool _isPointReached;

        protected override void Start()
        {
            base.Start();
        }

        public void Reset()
        {
            this._currentTime = this._countdown;
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this._isPointReached = false;
        }

        protected override void FixedUpdate()
        {
            if (this._isPointReached)
            {
                Attack();
            }
            else
            {
                Move(this._destination - (Vector2)this.transform.position);
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
                this._isPointReached = true;
                Debug.Log($"Point is reachable {_isPointReached}");
                return;
            }

            Vector2 _diretionToPlayer = direction.normalized * Time.fixedDeltaTime;
            Vector2 _nextPosition = _rigidbody.position + direction * _speed;
            _rigidbody.MovePosition(_nextPosition);
        }

        public override void Attack()
        {
            //Attack:
            if (this._target.GetHealth() <= 0)
                return;

            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0)
            {
                Vector2 _startPosition = this._firePoint.position;
                Vector2 _vector = (Vector2)this._target.transform.position - _startPosition;
                Vector2 _direction = _vector.normalized;

                _bulletManager.SpawnBullet(_startPosition, Color.red, this, _direction * 2);

                this._currentTime += this._countdown;
            }
        }
    }
}