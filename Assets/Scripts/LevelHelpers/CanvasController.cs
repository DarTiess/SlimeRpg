using UnityEngine;
using Zenject;

  public class CanvasController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject inGame;
        [SerializeField] private GameObject win;
        [SerializeField] private GameObject lost;
   
        GameManager _gameManager;

        [Inject]
        private void InitiallizeComponent(GameManager manager)
        {
            _gameManager = manager;                 
            _gameManager.OnLateWin += OnLevelWin;
            _gameManager.OnLateLost += OnLevelLost;
        }

        public void OnPlayGame()
        { 
            _gameManager.PlayGame();
            FalsePanels(); 
           inGame.SetActive(true);         
        }
        private void Start()
        {
            FalsePanels(); 
            mainMenu.SetActive(true);
        }

        private void OnLevelStart()
        {
            FalsePanels(); 
            mainMenu.SetActive(true);
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
            _gameManager.LoadNextLevel(); 
        }
        public void LevelStart()
        {
            _gameManager.LevelStart();
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
            _gameManager.RestartScene();
        }

    }


