using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemyLoader : MonoBehaviour
{
    public event Action<int> EnemyDeath; 
   private float _generateTimer;
   private List<Enemy> _enemyList;
    private int _indexEnemy = 0;
    private float _timer=0;
    private bool _canPush;
    
    public void Init(float timer,List<Enemy> enemyList)
    {
        _generateTimer = timer;
        _enemyList = new List<Enemy>(enemyList.Count);
        foreach (Enemy enemy in enemyList)
        {
            enemy.transform.parent = transform;
            enemy.EnemyDeath += OnDeathEnemy;
            _enemyList.Add(enemy);
        }

        _canPush = true;

    }

    private void OnDeathEnemy(int payment)
    {
       EnemyDeath?.Invoke(payment);
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

    public void ChangePoolingPosition(Transform newPosition)
    {
        gameObject.transform.position = newPosition.position;
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
}