using UnityEngine;

namespace InputHandlers
{
    public class KeyBoardInput : IInputHandler
    {
        public bool Up()
        {
            return Input.GetKeyDown(KeyCode.W);
        }

        public bool Down()
        {
            return Input.GetKeyDown(KeyCode.S);
        }

        public bool Left()
        {
            return Input.GetKeyDown(KeyCode.A);
        }

        public bool Right()
        {
            return Input.GetKeyDown(KeyCode.D);
        }
    }
}