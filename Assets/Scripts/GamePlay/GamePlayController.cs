using System;
using CoinsHandlers;
using Modules;
using SnakeGame;
using UnityEngine;

namespace GamePlay
{
    public class GamePlayController : IGamePlayController, IDisposable
    {
        public event Action OnLevelCompleted;
        
        private ISnake _snake;
        private ICoinsSpawner _coinsSpawner;
        
        private IScore _score;
        
        private IWorldBounds _worldBounds;
        
        private IDifficulty _difficulty;
        
        public GamePlayController(ISnake snake, ICoinsSpawner coinsSpawner, IDifficulty difficulty, IWorldBounds worldBounds)
        {
            _snake = snake;
            _coinsSpawner = coinsSpawner;
            _difficulty = difficulty;
            _worldBounds = worldBounds;
            
            _difficulty.OnStateChanged += DifficultyOnOnStateChanged;
            _snake.OnMoved += SnakeOnOnMoved;
        }

        private void SnakeOnOnMoved(Vector2Int obj)
        {
            var coins = _coinsSpawner.SpawnedCoins();

            if (coins == null)
                return;

            var coin = coins.Find(x => x.Position == obj);
            if (coin == null) return;
            
            _snake.Expand(1);
            _coinsSpawner.Despawn(coin);
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= DifficultyOnOnStateChanged;
            _snake.OnMoved -= SnakeOnOnMoved;
        }

        private void DifficultyOnOnStateChanged()
        {
            for (int i = 0; i < _difficulty.Current; i++)
            {
                _coinsSpawner.Spawn(_worldBounds.GetRandomPosition());
            }
            
            _snake.SetSpeed(_difficulty.Current);
            
        }
    }
}