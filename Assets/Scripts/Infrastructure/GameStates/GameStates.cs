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

        private LoadScene loadScene;
        private bool onPaused;


        public void Init(LoadScene _loadScene)
        {
            loadScene = _loadScene;
            LevelStart();
        }

        public void LevelStart()
        {
            Debug.Log("StartLevel");
            OnLevelStart?.Invoke();
        }

        public void PlayGame()
        {
            Debug.Log("PlayLevel");
            OnPlayGame?.Invoke();

        }

        public void LevelLost()
        {
            OnLevelLost?.Invoke();
        }

        public void LoadNextLevel()
        {
            loadScene.LoadNextLevel();
        }

        public void RestartScene()
        {
            loadScene.RestartScene();
        }

        public void PauseGame()
        {
            if (!onPaused)
            {
                StopGame?.Invoke();
                onPaused = true;
            }
            else
            {
                PlayGame();
                onPaused = false;
            }


        }
    }
}


