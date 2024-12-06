using System;
using CoinsHandlers;
using GameStateUI;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace GameCycle
{
    public class GameCycleController : IGameCycleController, IInitializable, IDisposable
    {
        private IDifficulty _difficulty;
        private ISnake _snake;
        private ICoinsSpawner _coinsSpawner;
        private IGameState _gameState;
        
        public GameCycleController(IDifficulty difficulty, ISnake snake, ICoinsSpawner coinsSpawner, IGameState gameState)
        {
            _difficulty = difficulty;
            _snake = snake;
            _coinsSpawner = coinsSpawner;
            _gameState = gameState;
        }
        
        public void Initialize()
        {
            StartGame();
            
            _snake.OnSelfCollided += SnakeOnSelfCollided;
            _coinsSpawner.OnCoinDeSpawned += CoinsSpawnerOnCoinDeSpawned;
        }

        private void CoinsSpawnerOnCoinDeSpawned()
        {
            if (_coinsSpawner.SpawnedCoins().Count == 0)
            {
                if(SetNextLevel())
                    return;
                
                _gameState.ActivateStateScreen(GameState.GAMEWIN);
            }
        }

        private void SnakeOnSelfCollided()
        {
            _gameState.ActivateStateScreen(GameState.GAMEOVER);
        }

        public void Dispose()
        {
            _snake.OnSelfCollided -= SnakeOnSelfCollided;
        }
        
        public void StartGame()
        {
            Debug.Log("Starting Game");
            SetNextLevel();
        }

        public bool SetNextLevel()
        {
            return _difficulty.Next(out var nextLevel);
        }

        public void GameOver()
        {
            throw new System.NotImplementedException();
        }

    }
}