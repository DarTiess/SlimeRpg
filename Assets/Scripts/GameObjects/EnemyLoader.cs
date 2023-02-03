using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class EnemyLoader : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int _enemyCount;
    [SerializeField] private List<Enemy> _enemyPrefab;
    [SerializeField] private List<string> _enemyKies;
    [SerializeField] private float _speed;

    [SerializeField] private float _generateTimer;

    private List<Enemy> _enemyList = new List<Enemy>();
    private int _indexEnemy = 0;
    private float _timer;
    private Player _player;
    private UIDisplay _money;
    private bool _canPush;

    [Inject]
    private void Construct(Player playerObj, UIDisplay moneyObj)
    {
        _player = playerObj;
        _money = moneyObj;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitiallizeAdressablesEnemy();
    }

    void Update()
    {
        if (!_canPush)
        {
            return;
        }
        if (_timer < _generateTimer)
        {
            _timer += Time.deltaTime;
            return;
        }

        PushEnemy();
    }

    private void PushEnemy()
    {
        if (_indexEnemy >= _enemyList.Count)
        {
            _indexEnemy = 0;
        }

        _enemyList[_indexEnemy].transform.position = gameObject.transform.position;

        _enemyList[_indexEnemy].gameObject.SetActive(true);
        _enemyList[_indexEnemy].PushEnemy();
        _indexEnemy++;
        _timer = 0;
        return;
    }
    private async void InitiallizeAdressablesEnemy()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            int indPref = 0;
            if (i > 2)
            {
                indPref = Random.Range(0, _enemyKies.Count);
            }

            var asyncPref = Addressables.LoadAssetAsync<GameObject>(_enemyKies[indPref]);
            await asyncPref.Task;
            CreateEnemy(asyncPref.Result);
        }
        _timer = _generateTimer;
        _canPush = true;
        return;
    }

    private void CreateEnemy(GameObject obj)
    {
        GameObject enemy = Instantiate(obj, transform.position, transform.rotation);
        enemy.SetActive(false);

        Enemy en = enemy.GetComponent<Enemy>();
        en.InitializeEnemy(_player, _money);
        _enemyList.Add(en);
    }
}
