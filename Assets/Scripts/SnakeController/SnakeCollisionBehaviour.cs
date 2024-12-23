using System;
using Coins;
using Modules;
using SnakeGame;
using UnityEngine;
using Uttilities;

namespace SnakeController
{
    public class SnakeCollisionBehaviour : ICollision
    {
        private ISnake _snake;
        private IWorldBounds _bounds;
        
        private ICoinsSpawner _coinsSpawner;
        
        public event Action OnCollision;

        public SnakeCollisionBehaviour(ISnake snake, IWorldBounds bounds, ICoinsSpawner coinsSpawner)
        {
            _snake = snake;
            _bounds = bounds;
            _coinsSpawner = coinsSpawner;
            
            _snake.OnMoved += SnakeOnMoved;
            _snake.OnSelfCollided += OnSelfCollided;
        }

        private void OnSelfCollided()
        {
            OnCollision?.Invoke();
        }

        private void SnakeOnMoved(Vector2Int obj)
        {
            if(!_bounds.IsInBounds(obj))
                OnCollision?.Invoke();
            
            if(!_coinsSpawner.CanGetCoin(obj))
                return;
            
            _snake.Expand(1);
        }
    }
}