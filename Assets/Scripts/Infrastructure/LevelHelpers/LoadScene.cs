using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.LevelHelpers
{
    public class LoadScene
    {
        private ISceneData _data;
        private List<string> _sceneNames = new List<string>();

        public LoadScene(ISceneData data, SceneSetting settings)
        {
            _data = data;
            foreach (string scene in settings.scenes)
            {
                _sceneNames.Add(scene);
            }
        }

        public void StartGame()
        {
            if (_data.CurrentScene == 0)
            {
                _data.CurrentScene= 1;
            }
       
            Loading();
        }

        public void Loading()
        {
            if (_data.CurrentScene == 0)
            {
                _data.CurrentScene= 1;
            }

            int numLoadedScene = _data.CurrentScene;
            if (numLoadedScene <= _sceneNames.Count)
            {
                numLoadedScene -= 1;
            }
            else
            {
                numLoadedScene = 0;

            }
            Debug.Log("Load Scene  " + numLoadedScene);
       
            SceneManager.LoadScene(_sceneNames[numLoadedScene]);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    
    }
}