using UnityEngine;

namespace Infrastructure.GameStates
{
    public interface IGameStates
    {
        void PlayGame();
        void LevelLost();
        void RestartScene();
        void PauseGame();
        void OnCreatePlane(Transform endPoint);
        void OnEnemyDeath(int payment);
    }
}