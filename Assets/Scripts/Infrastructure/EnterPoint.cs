using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Enemy;
using Environement;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class EnterPoint : MonoBehaviour
    {
        
        [Header("Player Settings")]
        [SerializeField] private Player.Player _playerPrefab;
        [SerializeField] private Transform _playerStartPosition;
        [Header("SceneLoader")]
        [SerializeField] private LoadScene _loadScene;
        [Header("UI")]
        [FormerlySerializedAs("_canvasPrefab")]
        [SerializeField] private UIController _uiPrefab;
        [Header("Environement Settings")]
        [SerializeField] private Transform _startEnvironementPosition;
        [SerializeField] private Environement.Environement _environementPrefab;
        [SerializeField] private int _countPlanes;
        [SerializeField] private NavMeshSettings _navMeshSettings;
        [Header("Enemy Loading")]
        [SerializeField] private EnemyFactorySettings _enemyFactorySettings;
        [SerializeField] private EnemyLoader _enemyLoaderPrefab;
        [SerializeField] private Transform _enemyLoaderPosition;

        private Player.Player _player;
        private CameraFollow.CameraFollow _mainCamera;
        private GameStates.GameStates _gameState;
        private UIController _ui;
        private List<Environement.Environement> _environements;
        private EnvironementLoader _environementLoader;
        private EnemyLoader _enemyLoader;
        private EnemyFactory _enemyFactory;
        private DataSaver _dataSaver;

        private async void Awake()
        {
            _dataSaver = new DataSaver();
            CreateGameStates();
            CreatePlayer();
            CreateUI();
            
            InitCamera();

            CreateEnvironementsList();
            CreateEnvironementLoader();

            CreateEnemyFactory();
            await CreateEnemyLoader();

            _gameState.Init(_loadScene);
            DontDestroyOnLoad(this);
        }

        private void CreateGameStates()
        {
            _gameState = new GameStates.GameStates();
        }

        private void CreateUI()
        {
            _ui = Instantiate(_uiPrefab);
            _ui.Init(_gameState, _gameState, _dataSaver);
        }

        private void CreatePlayer()
        {
            _player = Instantiate(_playerPrefab, _playerStartPosition.position, _playerStartPosition.rotation);
            _player.Init(_gameState, _gameState, _dataSaver);
        }

        private void InitCamera()
        {
            _mainCamera = Camera.main.GetComponent<CameraFollow.CameraFollow>();
            _mainCamera.Init(_player);
        }

        private void CreateEnvironementsList()
        {
            _environements = new List<Environement.Environement>(_countPlanes);
            for (int i = 0; i < _countPlanes; i++)
            {
                Environement.Environement environement = Instantiate(_environementPrefab, _startEnvironementPosition.position, _startEnvironementPosition.rotation);
                _environements.Add(environement);
            }
        }

        private void CreateEnvironementLoader()
        {
            _environementLoader = new EnvironementLoader(_environements, _gameState, _navMeshSettings);
        }

        private void CreateEnemyFactory()
        {
            _enemyFactory = new EnemyFactory(_enemyFactorySettings, _player);
        }

        private async Task CreateEnemyLoader()
        {
            _enemyLoader = Instantiate(_enemyLoaderPrefab, _enemyLoaderPosition.position, _enemyLoaderPosition.rotation);
            _enemyLoader.Init(_enemyFactorySettings._timerPooling, 
                              await _enemyFactory.CreateEnemiesPool(), 
                              _gameState, _gameState);
            
        }
    }
}