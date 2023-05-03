using Enemy;
using Environement;
using Infrastructure.GameStates;
using UI;
using UnityEngine;
using Zenject;

    public class GameComponentInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gameManager;
        [SerializeField] private GameObject environementLoader;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemyLoader;
        [SerializeField] private GameObject navMeshSettings;
        [SerializeField] private GameObject state;
        public override void InstallBindings()
        {
            Container.Bind<GameStates>().FromComponentOn(gameManager).AsSingle();
            Container.Bind<EnvironementLoader>().FromComponentOn(environementLoader).AsSingle();
            Container.Bind<Player.Player>().FromComponentOn(player).AsSingle();
            Container.Bind<EnemyLoader>().FromComponentOn(enemyLoader).AsSingle();
            Container.Bind<NavMeshSettings>().FromComponentOn(navMeshSettings).AsSingle();
            Container.Bind<DisplayUIState>().FromComponentOn(state).AsSingle();
        }
    }

