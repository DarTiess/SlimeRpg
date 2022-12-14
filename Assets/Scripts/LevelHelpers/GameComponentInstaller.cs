using UnityEngine;
using Zenject;

    public class GameComponentInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gameManager;
        [SerializeField] private GameObject environementLoader;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject enemyLoader;
        [SerializeField] private GameObject navMeshSettings;
        [SerializeField] private GameObject money;
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentOn(gameManager).AsSingle();
            Container.Bind<EnvironementLoader>().FromComponentOn(environementLoader).AsSingle();
            Container.Bind<Player>().FromComponentOn(player).AsSingle();
            Container.Bind<EnemyLoader>().FromComponentOn(enemyLoader).AsSingle();
            Container.Bind<NavMeshSettings>().FromComponentOn(navMeshSettings).AsSingle();
            Container.Bind<Money>().FromComponentOn(money).AsSingle();
        }
    }

