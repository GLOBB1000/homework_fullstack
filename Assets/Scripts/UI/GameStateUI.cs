using System;
using GameCycleInstances;
using TMPro;
using UnityEngine;

namespace GameStateUI
{
    public enum GameState
    {
        GAMEOVER,
        GAMEWIN
    }
    public class GameStateUI : IDisposable
    {
        private GameObject _gameOverScreen;
        private GameObject _gameWinScreen;

        private IGameCycle _gameCycle;
         
        public GameStateUI(GameObject gameOverScreen, 
            GameObject gameWinScreen,
            IGameCycle gameCycle)
        {
            _gameOverScreen = gameOverScreen;
            _gameWinScreen = gameWinScreen;
            
            _gameCycle = gameCycle;
            
            _gameCycle.OnGameWon += OnGameWon;
            _gameCycle.OnGameLost += OnGameLost;
        }

        private void OnGameLost()
        {
            ActivateStateScreen(GameState.GAMEOVER);
        }

        private void OnGameWon()
        {
            ActivateStateScreen(GameState.GAMEWIN);
        }


        private void ActivateStateScreen(GameState newState)
        {
            switch (newState)
            {
                case GameState.GAMEOVER:
                    _gameOverScreen.SetActive(true);
                    break;
                case GameState.GAMEWIN:
                    _gameWinScreen.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        public void Dispose()
        {
            _gameCycle.OnGameWon -= OnGameWon;
            _gameCycle.OnGameLost -= OnGameLost;
        }
    }
}