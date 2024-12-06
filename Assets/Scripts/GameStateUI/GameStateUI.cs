using System;
using UnityEngine;

namespace GameStateUI
{
    public enum GameState
    {
        GAMEOVER,
        GAMEWIN
    }
    public class GameStateUI : IGameState
    {
        private GameObject _gameOverScreen;
        private GameObject _gameWinScreen;
         
        public GameStateUI(GameObject gameOverScreen, GameObject gameWinScreen)
        {
            _gameOverScreen = gameOverScreen;
            _gameWinScreen = gameWinScreen;
        }
        
        
        public void ActivateStateScreen(GameState newState)
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
    }
}