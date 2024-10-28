using System;
using UnityEngine;
using Ship;

namespace ShootEmUp
{
    public sealed class EnemyAI : MonoBehaviour
    {
        public ShipHandler ship;

        [SerializeField]
        private float _countdown;

        [NonSerialized]
        public Player Target;

        private Vector2 _destination;
        private float _currentTime;
        private bool _isPointReached;

        private void Start()
        {
            ship.ResetShipInstance();
            ship.OnShipDeath += ShipOnOnShipDeath;
        }

        private void ShipOnOnShipDeath()
        {
            ship.ResetShipInstance();
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
                Debug.Log($"Point is reachable {_isPointReached}");
                return;
            }

            ship.Move(direction, Time.fixedDeltaTime);
        }

        private void ShipAttack()
        {
            //Attack:
            if (Target.ShipHandlerInstance.GetHealth() <= 0)
                return;

            this._currentTime -= Time.fixedDeltaTime;
            if (this._currentTime <= 0)
            {
                ship.Attack(Color.red ,Target.transform);
                this._currentTime += this._countdown;
            }
        }
    }
}