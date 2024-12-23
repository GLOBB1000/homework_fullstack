using System;
using GameCycleInstances;
using Input;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeController
{
    public sealed class SnakeMovementController : ITickable, IDisposable
    {
        private ISnake _snake;
        private IInputHandler _inputHandler;
        
        private IGameCycle _gameCycle;

        public SnakeMovementController(ISnake snake, IInputHandler inputHandler, IGameCycle gameCycle)
        {
            _snake = snake;
            _inputHandler = inputHandler;
            
            _gameCycle = gameCycle;
            
            _gameCycle.OnLevelCleared += OnLevelCleared;
        }

        private void OnLevelCleared(int obj)
        {
            _snake.SetSpeed(obj);
        }

        public void Tick()
        {
            _snake.Turn(_inputHandler.GetDirection());
        }

        public void Dispose()
        {
            _gameCycle.OnLevelCleared -= OnLevelCleared;
        }
    }
}