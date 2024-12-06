using CoinsHandlers;
using GameStateUI;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace SnakeBehaviour
{
    public class SnakeCollisionBehaviour
    {
        private ISnake _snake;
        private IWorldBounds _bounds;
        
        private IGameState _gameState;

        public SnakeCollisionBehaviour(ISnake snake, IWorldBounds bounds, IGameState gameState)
        {
            _snake = snake;
            _bounds = bounds;
            _gameState = gameState;
            
            _snake.OnMoved += SnakeOnMoved;
        }

        private void SnakeOnMoved(Vector2Int obj)
        {
            if(_bounds.IsInBounds(obj))
                return;
            
            _gameState.ActivateStateScreen(GameState.GAMEOVER);
        }
    }
}