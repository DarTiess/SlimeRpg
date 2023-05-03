using System;
using UnityEngine;

namespace Infrastructure.GameStates
{
    public class GameStates : IGameStatesEvents, IGameStates
    {
        public event Action OnLevelStart;
        public event Action OnPlayGame;
        public event Action OnLevelLost;
        public event Action StopGame;
        public event Action<Transform> CreatePlane;
        public event Action<int> EnemyDeath;

        private LoadScene _loadScene;
        private bool _onPaused;


        public void Init(LoadScene loadScene)
        {
            _loadScene = loadScene;
            LevelStart();
        }

        public void LevelStart()
        {
            OnLevelStart?.Invoke();
        }

        public void PlayGame()
        {
            OnPlayGame?.Invoke();
        }

        public void LevelLost()
        {
            OnLevelLost?.Invoke();
        }

        public void LoadNextLevel()
        {
            _loadScene.LoadNextLevel();
        }

        public void RestartScene()
        {
            _loadScene.RestartScene();
        }

        public void PauseGame()
        {
            if (!_onPaused)
            {
                StopGame?.Invoke();
                _onPaused = true;
            }
            else
            {
                PlayGame();
                _onPaused = false;
            }
        }

        public void OnCreatePlane(Transform endPoint)
        {
           CreatePlane?.Invoke(endPoint);
        }

        public void OnEnemyDeath(int payment)
        {
            EnemyDeath?.Invoke(payment);
            PlayGame();
        }
    }
  
}


