using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
  
    public event Action OnLevelStart;
    public event Action OnPlayGame;
    public event Action OnLevelWin;
    public event Action OnLevelLost;
    public event Action OnLateLost;
    public event Action OnLateWin;
 

   
    [Space] 
    [Header("LevelLoader")] 
    public LoadScene loadScene;

    [SerializeField] private float timeWaitWin;
    [SerializeField] private float timeWaitLostn;
    private void Start()
    {
        LevelStart();
    }
 
    public void LevelStart()
    {
        Time.timeScale = 0;
        OnLevelStart?.Invoke();
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        OnPlayGame?.Invoke();

    }
    public void LevelLost()
    {
        OnLevelLost?.Invoke();
        Invoke(nameof(LateLost), timeWaitLostn);
    }

    private void LateLost()
    {
        OnLateLost?.Invoke();
        Time.timeScale = 0;
    }

    public void LevelWin()
    {
        OnLevelWin?.Invoke();
        Invoke(nameof(LateWin), timeWaitWin);
    }
    private void LateWin()
    {
        OnLateWin?.Invoke();
        Time.timeScale = 0;
    }

    public void LoadNextLevel()
    {
        loadScene.LoadNextLevel();
    }

    public void RestartScene()
    {
        loadScene.RestartScene();
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
    }

}


