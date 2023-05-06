using System;
using System.Collections;
using HealthBar;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyHealthBar))]
    public class Enemy : MonoBehaviour
    {
        public event Action<int> EnemyDeath;
        [SerializeField] private int _payment;
        [SerializeField] private int _makeDamage;
        [SerializeField] private ParticleSystem _splashEffect;
        [SerializeField] private int _health;

        private NavMeshAgent _navMesh;
        private EnemyHealthBar _healthBar;
        private Transform _player;
   
        private bool _canMove;
        private int _startHealth;
    
        void FixedUpdate()
        {
            if (_canMove)
            {
                _navMesh.SetDestination(_player.transform.position);
            }
        }

        public void InitializeEnemy(Transform playerObj)
        {
            _startHealth = _health;
            _player = playerObj;
     
            _navMesh = GetComponent<NavMeshAgent>();
            _healthBar = GetComponent<EnemyHealthBar>();
        }

        public void PushEnemy()
        {
            _health = _startHealth;
            _healthBar.Show();
            _healthBar.SetMaxValus(_health);
            _canMove = true;

            gameObject.tag = "Enemy";
            _navMesh.isStopped = false;
        }

        public void StopEnemy()
        {
            _canMove = false;
            _navMesh.isStopped = true;
           
        }
        IEnumerator DeadEnemy()
        {
            yield return new WaitForSeconds(0.2f);
            EnemyDeath?.Invoke(_payment); 
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Player.Bullet>(out Bullet bullet))
            {
                _splashEffect.transform.position = collision.transform.position;
                _splashEffect.Play();
                bullet.TryDestroy();
                TakeDamage(bullet.Damage);
                
            }

            if (collision.gameObject.TryGetComponent<IPlayerDamage>(out IPlayerDamage player))
            {
                player.TakeDamage(_makeDamage, gameObject.transform);
            }
        }
        private void TakeDamage(int damage)
        {
            _healthBar.SetValues(damage, 0.2f);
            _health -= damage;
            if (_health <= 0)
            {
                StopEnemy();
                StartCoroutine(DeadEnemy());
            }
            else
            {
                gameObject.tag = "Enemy";
            }
        }
    }
}
