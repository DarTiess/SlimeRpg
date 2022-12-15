using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyLoader : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private List<Enemy> enemyPrefab;
    private List<Enemy> _enemyList=new List<Enemy>();
    private int _indexEnemy = 0;
    [SerializeField]
    private float speed; 
  
    [SerializeField] private float generateTimer;
  
    float timer;
    private Player _player;
    private PlayerState _money;

    private bool _canPush;
    [Inject]
    private void InitiallizeComponent(Player playerObj, PlayerState moneyObj)
    {
        _player = playerObj;
        _money = moneyObj;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitiallizeEnemyList();
    }

    private void InitiallizeEnemyList()
    {
       
        for (int i = 0; i < enemyCount; i++)
        { 
            int indPref = 0;

            if (i > 2)
            {
                indPref = Random.Range(0,enemyPrefab.Count);
            }

            Enemy enemy = Instantiate(enemyPrefab[indPref], transform.position, transform.rotation);
            enemy.gameObject.SetActive(false); 
           
            enemy.InitializeEnemy(_player, _money);
            _enemyList.Add(enemy);

        }

        timer = generateTimer;
        _canPush= true;
        return;
    }
    // Update is called once per frame
    void Update()
    {
        if (!_canPush)
        {
            return;
        }
        if (timer < generateTimer)
        {
            timer += Time.deltaTime;
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
        
        _enemyList[_indexEnemy].transform.position =gameObject.transform.position;
       
        _enemyList[_indexEnemy].gameObject.SetActive(true); 
        _enemyList[_indexEnemy].PushEnemy();
        _indexEnemy++;
        timer = 0;
        return;
    }
}
