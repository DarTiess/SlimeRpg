using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyFactory
{
    private List<string> _enemyKeis=new List<string>();
    private int _enemyCount;
    private List<Enemy> _enemyList;
    private Transform _player;

    public EnemyFactory(EnemyFactorySettings settings, Player.Player player )
    {
        _enemyCount = settings.enemyCount;
        _enemyList = new List<Enemy>(_enemyCount);
        _player = player.transform;
       
        foreach (string key in settings.enemyKeys)
        {
            _enemyKeis.Add(key);
        }
    }
    
    public async Task<List<Enemy>> CreateEnemiesPool()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            int indPref = 0;
            if (i > 2)
            {
                indPref = Random.Range(0, _enemyKeis.Count);
            }

            var asyncPref = Addressables.LoadAssetAsync<GameObject>(_enemyKeis[indPref]);
            await asyncPref.Task;
            CreateEnemy(asyncPref.Result);           
        }

        return _enemyList;
    }

    private void CreateEnemy(GameObject obj)
    {
        GameObject enemy =Object.Instantiate(obj);
        enemy.SetActive(false);

        Enemy en = enemy.GetComponent<Enemy>();
        en.InitializeEnemy(_player);
        _enemyList.Add(en);
    }
}