using Modules;
using UnityEngine;

namespace Input
{
    public class KeyBoardInput : IInputHandler
    {
        public SnakeDirection GetDirection()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
                return SnakeDirection.UP;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.S))
                 return SnakeDirection.DOWN;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
                return SnakeDirection.LEFT;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.D))
                return SnakeDirection.RIGHT;
            
            else
                return SnakeDirection.NONE;
        }
    }
}