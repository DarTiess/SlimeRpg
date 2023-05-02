namespace Infrastructure.GameStates
{
    public interface IGameStates
    {
        void LevelStart();
        void PlayGame();
        void LevelLost();
        void LoadNextLevel();
        void RestartScene();
        void PauseGame();
    }
}