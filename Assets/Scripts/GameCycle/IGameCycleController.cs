namespace GameCycle
{
    public interface IGameCycleController
    {
        public void StartGame();
        
        public bool SetNextLevel();
        
        public void GameOver();
    }
}