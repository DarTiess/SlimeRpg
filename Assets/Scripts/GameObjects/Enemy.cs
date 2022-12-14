using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(HealthBar))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    private HealthBar healthBar;
    private Player player;
    private Money money;
    private bool _canMove;

    [SerializeField]
    private int health;
    [SerializeField]
    private int payment;
    [SerializeField]
    private int makeDamage;
    [SerializeField]private ParticleSystem splashEffect;
    // Update is called once per frame
    void LateUpdate()
    {
        if (_canMove)
        { 
            _navMesh.SetDestination(player.transform.position);
        }
    }

    public void InitializeEnemy(Player playerObj, Money moneyObj)
    {
        player=playerObj;
        money=moneyObj;
        _navMesh= GetComponent<NavMeshAgent>();
        healthBar = GetComponent<HealthBar>();
    }

    public void PushEnemy()
    {
        healthBar.SetOnSlider();
        healthBar.SetMaxValus(health);
        _canMove=true;
        _navMesh.isStopped = false;
    }

    public void StopEnemy()
    {
        money.AddCoins(payment);
       // healthBar.SetOffSlider();
     
        _canMove=false;
        _navMesh.isStopped = true;
       StartCoroutine(DeadEnemy());
    }

    IEnumerator DeadEnemy()
    {
        yield return new WaitForSeconds(0.5f); 
        player.StartMove();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball");
            splashEffect.transform.position=collision.transform.position;
            splashEffect.Play();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collis");
            player.TakeDamage(makeDamage, gameObject.transform);
        }
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetValues(damage, 0.2f);
        health-=damage;
      
        if (health <= 0)
        {
            StopEnemy();
        }
        else
        {
            gameObject.tag = "Enemy";
        }
    }
}
