using System;
using InputHandlers;
using UnityEngine;
using Ships;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField]
        private Ship ship;

        private bool isFireRequired;

        private void Update()
        {
            if(InteractInputHandler.Instance.Shoot())
                ship.Attack();
        }
    }
}