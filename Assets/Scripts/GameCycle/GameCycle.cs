using System;

namespace GameCycleInstances
{
    public class GameCycle : IGameCycle
    {
        public event Action OnGameStarted;
        public event Action OnGameWon;
        public event Action OnGameLost;
        
        public event Action<int> OnLevelCleared;

        public void StartGame()
        {
            OnGameStarted?.Invoke();
        }

        public void WinGame()
        {
            OnGameWon?.Invoke();
        }

        public void LoseGame()
        {
            OnGameLost?.Invoke();
        }

        public void ClearLevel(int level)
        {
            OnLevelCleared?.Invoke(level);
        }
    }
}