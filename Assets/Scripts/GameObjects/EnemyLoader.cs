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
    private Enemy enemyPrefab;
    private List<Enemy> _enemyList=new List<Enemy>();
    private int _indexEnemy = 0;
    [SerializeField]
    private float speed; 
  
    [SerializeField] private float generateTimer;
  
    float timer = 0;
    private Player _player;
    private Money _money;

    private bool _canPush;
    [Inject]
    private void Initialization(Player playerObj, Money moneyObj)
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
            Enemy enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            enemy.gameObject.SetActive(false); 
           
            enemy.InitializeEnemy(_player, _money);
            _enemyList.Add(enemy);

        }
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
