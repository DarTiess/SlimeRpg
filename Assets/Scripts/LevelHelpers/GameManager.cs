using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeRpg
{
public class GameManager : MonoBehaviour
{
    public event Action StoreSouls;
    public event Action AddSoul;
    public event Action FreeSoul;
    public event Action OnLevelStart;
    public event Action OnLevelWin;
    public event Action OnLevelLost;
    public event Action OnLateLost;
    public event Action OnLateWin;
 

   
    [Space] 
    [Header("LevelLoader")] 
    public LoadScene loadScene;

    private int takenSoul=0;
    [SerializeField] private float timeWaitWin;
    [SerializeField] private float timeWaitLostn;
    private void Start()
    {
       Time.timeScale = 0.1f;
    }
    public void StartStoringSouls()
    {
        StoreSouls?.Invoke();
    }
    public void LevelStart()
    {
        Time.timeScale = 1;
        takenSoul = 0;
        wrong = false;
        OnLevelStart?.Invoke();
    }

    public void LevelLost()
    {
        takenSoul = 0;
        wrong = false;
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
        takenSoul = 0;
        wrong = false;
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

    public void AddTakenSoul()
    {
        takenSoul+=1;
        AddSoul?.Invoke();
    }
    bool wrong = false;
    public void SetFreeSoul(bool right)
    { 
        takenSoul-=1;
        FreeSoul?.Invoke();
        if (!right)
        {
            wrong = true;
        }
      
        if(takenSoul<=0)
        {
            if (wrong)
            {
                LevelLost();
            }
            else
            {
                LevelWin();
            }
           
        }
    }

   
}
}

