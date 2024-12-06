using InputHandlers;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeBehaviour
{
    public sealed class SnakeMovementController : ITickable
    {
        private ISnake _snake;
        private IInputHandler _inputHandler;

        public SnakeMovementController(ISnake snake, IInputHandler inputHandler)
        {
            _snake = snake;
            _inputHandler = inputHandler;
        }
        
        public void Tick()
        {
            if(_inputHandler.Up())
                _snake.Turn(SnakeDirection.UP);
            else if(_inputHandler.Down())
                _snake.Turn(SnakeDirection.DOWN);
            else if(_inputHandler.Left())
                _snake.Turn(SnakeDirection.LEFT);
            else if(_inputHandler.Right())
                _snake.Turn(SnakeDirection.RIGHT);
            else
                _snake.Turn(SnakeDirection.NONE);
        }
    }
}