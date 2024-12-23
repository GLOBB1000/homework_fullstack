using System;
using Coins;
using Modules;
using Uttilities;
using Zenject;

namespace GameCycleInstances
{
    public class GameCycleController : IInitializable, IDisposable
    {
        private IGameCycle _gameCycle;

        private ICoinsSpawner _coinsSpawner;
        private ICollision _collision_component;
        
        private IDifficulty _difficulty;
        
        public GameCycleController(ICollision collision, ICoinsSpawner coinsSpawner, IGameCycle gameCycle, IDifficulty difficulty)
        {
            _gameCycle = gameCycle;
            _difficulty = difficulty;

            _collision_component = collision;

            _coinsSpawner = coinsSpawner;

            _collision_component.OnCollision += GameOver;
        }
        
        public void Initialize()
        {
            _gameCycle.StartGame();
            _coinsSpawner.OnCoinDeSpawned += CoinsSpawnerOnCoinDeSpawned;

            SetNextLevel();
        }

        private void CoinsSpawnerOnCoinDeSpawned()
        {
            if (_coinsSpawner.CountOfCoins() != 0) return;

            if(SetNextLevel())
                return;
            
            _gameCycle.WinGame();
        }
        
        private bool SetNextLevel()
        {
            var canMoveToNext = _difficulty.Next(out var nextLevel);
            
            _gameCycle.ClearLevel(nextLevel);
            
            return canMoveToNext;
        }

        public void Dispose()
        {
            _collision_component.OnCollision -= GameOver;
            _coinsSpawner.OnCoinDeSpawned -= CoinsSpawnerOnCoinDeSpawned;
        }

        private void GameOver()
        {
            _gameCycle.LoseGame();
        }

    }
}