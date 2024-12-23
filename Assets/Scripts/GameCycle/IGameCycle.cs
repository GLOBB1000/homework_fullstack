using System;

namespace GameCycleInstances
{
    public interface IGameCycle
    {
        event Action OnGameStarted;
        
        event Action OnGameWon;
        
        event Action OnGameLost;

        event Action<int> OnLevelCleared;
        
        void StartGame();
        
        void WinGame();
        
        void LoseGame();

        void ClearLevel(int level);

    }
}