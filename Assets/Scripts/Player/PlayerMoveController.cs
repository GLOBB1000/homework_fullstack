using InputHandlers;
using Ships;
using UnityEngine;

namespace Player
{
    public sealed class PlayerMoveController : MonoBehaviour
    {
        [SerializeField]
        private Ship ship;

        private bool fireRequired;

        private void FixedUpdate()
        {
            if (MoveInputHandler.Instance.MoveLeft())
                ship.Move(new Vector2(-1, 0), Time.fixedDeltaTime);
            
            else if (MoveInputHandler.Instance.MoveRight())
                ship.Move(new Vector2(1, 0), Time.fixedDeltaTime);
            
            else
                ship.Move(new Vector2(0, 0), Time.fixedDeltaTime);
        }
    }
}