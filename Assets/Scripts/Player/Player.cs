using System;
using Intrefaces;
using Ship;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour
    {
        public ShipHandler ShipHandlerInstance;

        private float _moveDirection;

        private void Start()
        {
            ShipHandlerInstance.ResetShipInstance();
        }

        public void Attack()
        {
            Debug.Log("Attack");
            ShipHandlerInstance.Attack(Color.blue);
        }

        public void SetDirection(float direction)
        {
            ShipHandlerInstance.Move(new Vector2(direction, 0), Time.fixedDeltaTime);
        }
    }
}