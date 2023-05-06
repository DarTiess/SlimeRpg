using Data;
using Infrastructure.GameStates;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(UIPlayerStates))]
    public class UIController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private StartMenu _mainMenu;
        [SerializeField] private GamePanel _inGame;
        [SerializeField] private LostPanel _lost;
   
        private IGameStatesEvents _gameEvents;
        private IGameStates _gameStates;
        private IDataSaver _data;
        private UIPlayerStates _uiPlayerStateses;
        private UIPlayerUpgradeWindow _uiPlayerUpgradeWindow;

        public void Init(IGameStatesEvents gameEvents, IGameStates gameStates, IDataSaver data)
        {
            _gameEvents = gameEvents;
            _gameStates = gameStates;
            _gameEvents.OnLevelStart += OnLevelStart;
            _gameEvents.OnLevelLost += OnLevelLost;
            _gameEvents.EnemyDeath += AddCoins;
            
            _data = data;
            
            _uiPlayerStateses = GetComponent<UIPlayerStates>();
            _uiPlayerStateses.Init(_data);
            _uiPlayerStateses.PayCoins += OnPayCoins;

            _uiPlayerUpgradeWindow = GetComponent<UIPlayerUpgradeWindow>();
            _uiPlayerUpgradeWindow.Init(_uiPlayerStateses);
            _uiPlayerUpgradeWindow.MakeUpgrade += TryMakePlayerUpgrade;
            
            _mainMenu.ClickedPanel += OnPlayGame;
            _lost.ClickedPanel += RestartGame;
            _inGame.ClickedPanel += OnPauseGame;
        }

        private void OnPayCoins(int coins)
        {
            _data.PayCoins(coins);
        }

        private void TryMakePlayerUpgrade(UpgradesType typeUpgrade, int value)
        {
            switch (typeUpgrade)
            {
                case UpgradesType.HP:
                    _uiPlayerStateses.AddHp(value);
                    break;
                case UpgradesType.AttackPower:
                    _uiPlayerStateses.AddAttack(value);
                    break;
            }
            _data.OnMakeUpgrade(typeUpgrade, value);
        }

        private void OnDisable()
        {
            _gameEvents.OnLevelStart -= OnLevelStart;
            _gameEvents.OnLevelLost -= OnLevelLost;
            _gameEvents.EnemyDeath -= AddCoins;
            _uiPlayerUpgradeWindow.MakeUpgrade -= TryMakePlayerUpgrade;
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
            _uiPlayerStateses.AddCoins(payment);
            _data.CoinNum += payment;
        }
    }
}


