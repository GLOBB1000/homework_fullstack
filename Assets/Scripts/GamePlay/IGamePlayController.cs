using System;

namespace GamePlay
{
    public interface IGamePlayController
    {
        event Action OnLevelCompleted; 
    }
}