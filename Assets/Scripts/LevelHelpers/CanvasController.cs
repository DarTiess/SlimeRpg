using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SlimeRpg
{
    public class CanvasController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject win;
        [SerializeField] private GameObject lost;
   

   
        int numLevel;
        GameManager gameManager;

        [Inject]
        private void Init(GameManager manager)
        {
            gameManager = manager;
            gameManager.OnLevelStart += OnLevelStart;
            gameManager.OnLateWin += OnLevelWin;
            gameManager.OnLateLost += OnLevelLost;
        }

   

        private void Start()
        {
            FalsePanels();
            numLevel = gameManager.loadScene.numScene;
            mainMenu.SetActive(true);
      
        }

        private void OnLevelStart()
        {
            FalsePanels(); 
            inGame.SetActive(true);
        }

        private void OnLevelWin()
        {
            Debug.Log("Level Win");
       
            FalsePanels();
            win.SetActive(true);
       
        }

        private void OnLevelLost()
        {
            Debug.Log("Level Lost");
            FalsePanels(); 
            lost.SetActive(true);
        }
  
        public void LoadNextLevel()
        {
            gameManager.LoadNextLevel(); 
        }
        public void LevelStart()
        {
            gameManager.LevelStart();
        }

        private void FalsePanels()
        {
            mainMenu.SetActive(false);
            inGame.SetActive(false);
            win.SetActive(false);
            lost.SetActive(false);
        }

 

        public void RestartGame()
        {
            gameManager.RestartScene();
        }

    }

}
