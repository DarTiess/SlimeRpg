using System.Collections.Generic;
using Infrastructure.GameStates;
using UnityEngine;

namespace Enemy
{
    public class EnemyLoader : MonoBehaviour
    {
        private float _generateTimer;
        private List<Enemy> _enemyList;
        private int _indexEnemy = 0;
        private float _timer=0;
        private bool _canPush;

        private IGameStatesEvents _gameStatesEvents;
        private IGameStates _gameStates;

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
        public void Init(float timer,List<Enemy> enemyList, IGameStatesEvents gameStatesEvents, IGameStates gameStates)
        {
            _generateTimer = timer;
            _gameStatesEvents = gameStatesEvents;
            _gameStates = gameStates;
            _gameStatesEvents.CreatePlane += ChangePoolingPosition;
            _gameStatesEvents.OnLevelLost += StopGame;
            CreateEnemyList(enemyList);
        }

        private void CreateEnemyList(List<Enemy> enemyList)
        {
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
            _gameStates.OnEnemyDeath(payment);
        }

        private void ChangePoolingPosition(Transform newPosition)
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
        }

        private void StopGame()
        {
            _canPush = false;
            foreach (Enemy enemy in _enemyList)
            {
                if(enemy.isActiveAndEnabled)
                {
                    enemy.StopEnemy();
                }
            }
        }
    }
}