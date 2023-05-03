using System;
using Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [RequireComponent(typeof(DisplayUIState))]
    public class UIController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private StartMenu _mainMenu;
        [SerializeField] private GamePanel _inGame;
        [SerializeField] private LostPanel _lost;
   
        private IGameStatesEvents _gameEvents;
        private IGameStates _gameStates;
        private DisplayUIState _displayUiStates;

        private void Start()
        {
            _displayUiStates = GetComponent<DisplayUIState>();
        }

        public void Init(IGameStatesEvents gameEvents, IGameStates gameStates)
        {
            _gameEvents = gameEvents;
            _gameStates = gameStates;
            
            _gameEvents.OnLevelStart += OnLevelStart;
            _gameEvents.OnLevelLost += OnLevelLost;
            _gameEvents.EnemyDeath += AddCoins;
            
            _mainMenu.ClickedPanel += OnPlayGame;
            _lost.ClickedPanel += RestartGame;
            _inGame.ClickedPanel += OnPauseGame;
        }

        private void OnDisable()
        {
            _gameEvents.OnLevelStart -= OnLevelStart;
            _gameEvents.OnLevelLost -= OnLevelLost;
            _gameEvents.EnemyDeath -= AddCoins;
            
            _mainMenu.ClickedPanel -= OnPlayGame;
            _lost.ClickedPanel -= RestartGame;
            _inGame.ClickedPanel -= OnPauseGame;
        }

        private void OnPauseGame()
        {
            _gameStates.PauseGame();
        }

        private void OnPlayGame()
        { 
            _gameStates.PlayGame();
            HideAllPanels(); 
            _inGame.Show();         
        }

        private void OnLevelStart()
        {
            HideAllPanels(); 
            _mainMenu.Show();
        }

        private void OnLevelLost()
        {
            Debug.Log("Level Lost");
            HideAllPanels(); 
            _lost.Show();
        }

        private void HideAllPanels()
        {
            _mainMenu.Hide();
            _inGame.Hide();
            _lost.Hide();
        }

        private void RestartGame()
        {
            _gameStates.RestartScene();
        }

        private void AddCoins(int payment)
        {
            _displayUiStates.AddCoins(payment);
        }
    }
}


