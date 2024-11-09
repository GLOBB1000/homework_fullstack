using System;
using UnityEngine;
using Ships;
using UnityEngine.Serialization;

namespace Enemy
{
    public sealed class EnemyAI : MonoBehaviour
    {
        public Ship AIShip;

        [SerializeField] private float countdown;

        [NonSerialized] public Ship PlayerShip;

        private Vector2 _destination;
        private float _currentTime;
        private bool _isPointReached;

        private void Start()
        {
            AIShip.ResetShipInstance();
            AIShip.OnShipDeath += AIShipOnOnAIShipDeath;
        }

        private void AIShipOnOnAIShipDeath()
        {
            AIShip.ResetShipInstance();
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this._isPointReached = false;
        }

        private void FixedUpdate()
        {
            if (_isPointReached)
            {
                ShipAttack();
            }
            else
            {
                MoveShip(this._destination - (Vector2)this.transform.position);
            }
        }

        private void MoveShip(Vector2 direction)
        {
            if (direction.magnitude <= 0.25f)
            {
                this._isPointReached = true;
                return;
            }

            AIShip.Move(direction, Time.fixedDeltaTime);
        }

        private void ShipAttack()
        {
            //Attack:
            if (PlayerShip.GetHealth() <= 0)
                return;

            this._currentTime -= Time.fixedDeltaTime;
            
            if (this._currentTime <= 0)
            {
                AIShip.Attack(PlayerShip.transform);
                this._currentTime += this.countdown;
            }
        }
    }
}