using System;
using GameCycleInstances;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Coins
{
    public class CoinsSpawnerController : IDisposable
    {
        private IGameCycle _gameCycle;
        private IWorldBounds _bounds;
        
        private ICoinsSpawner _coinsSpawner;
        
        public CoinsSpawnerController(IGameCycle gameCycle, IWorldBounds bounds, ICoinsSpawner coinsSpawner)
        {
            _gameCycle = gameCycle;
            _bounds = bounds;
            
            _coinsSpawner = coinsSpawner;

            _gameCycle.OnLevelCleared += OnLevelCleared;
        }

        private void OnLevelCleared(int level)
        {
            SpawnAmountOfCoins(level);
        }

        private void SpawnAmountOfCoins(int amountOfCoins)
        {
            for (int i = 0; i < amountOfCoins; i++)
            {
                _coinsSpawner.CreateCoin(_bounds.GetRandomPosition());
            }
        }

        public void Dispose()
        {
            _gameCycle.OnLevelCleared -= OnLevelCleared;
        }
    }
}