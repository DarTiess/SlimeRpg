using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthBar))]
public class Enemy : MonoBehaviour
{
    public event Action<int> EnemyDeath;
    public event Action<int, Enemy> EnemyAttack;
    [SerializeField] private int _payment;
    [SerializeField] private int _makeDamage;
    [SerializeField] private ParticleSystem _splashEffect;
    [SerializeField] private int _health;

    private NavMeshAgent _navMesh;
    private HealthBar _healthBar;
    private Transform _player;
   
    private bool _canMove;
    private int _startHealth;
    
    void LateUpdate()
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
        _healthBar = GetComponent<HealthBar>();
    }

    public void PushEnemy()
    {
        _health = _startHealth;
        _healthBar.SetOnSlider();
        _healthBar.SetMaxValus(_health);
        _canMove = true;

        gameObject.tag = "Enemy";
        _navMesh.isStopped = false;
    }

    public void StopEnemy()
    {
      
        _canMove = false;
        _navMesh.isStopped = true;
        StartCoroutine(DeadEnemy());
    }

    IEnumerator DeadEnemy()
    {
        yield return new WaitForSeconds(0.2f);
      //  _player.StartMove();
         EnemyDeath?.Invoke(_payment); 
       gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            _splashEffect.transform.position = collision.transform.position;
            _splashEffect.Play();
        }

        if (collision.gameObject.TryGetComponent<Player.Player>(out Player.Player player))
        {
            player.TakeDamage(_makeDamage, gameObject.transform);
        }
    }
    public void TakeDamage(int damage)
    {
        _healthBar.SetValues(damage, 0.2f);
        _health -= damage;

        if (_health <= 0)
        {
            StopEnemy();
        }
        else
        {
            gameObject.tag = "Enemy";
        }
    }
}
