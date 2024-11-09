using UnityEngine;

namespace InputHandlers
{
    public class KeyboardMoveHandler : MoveInputHandler
    {
        public override bool MoveLeft()
        {
            return Input.GetKey(KeyCode.LeftArrow);
        }

        public override bool MoveRight()
        {
            return Input.GetKey(KeyCode.RightArrow);
        }
    }
}
