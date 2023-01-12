using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadScene", menuName = "LoadScene", order = 51)]
public class LoadScene : ScriptableObject
{
    public List<string> sceneNames;
    public int numScene
    {
        get { return PlayerPrefs.GetInt("NumberScene"); }
        set { PlayerPrefs.SetInt("NumberScene", value); }
    }

    public void StartGame()
    {
        if (numScene == 0)
        {
            numScene = 1;
        }
       
        Loading();
    }

    public void LoadNextLevel()
    {
        numScene += 1;
        Loading();
    }

    public void Loading()
    {
        int numLoadedScene = numScene;
        if (numLoadedScene <= sceneNames.Count)
        {
            numLoadedScene -= 1;
        }
        else
        {
            numLoadedScene = 0;

        }
        Debug.Log("Load Scene  " + numLoadedScene);
       
        SceneManager.LoadScene(sceneNames[numLoadedScene]);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
