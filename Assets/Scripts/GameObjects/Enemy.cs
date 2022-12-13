using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    private GameObject player;
    private bool _canMove;

    // Update is called once per frame
    void Update()
    {
        if (_canMove)
        {
            _navMesh.SetDestination(player.transform.position);
        }
       
    }

    public void InitializeEnemy(GameObject playerObj, float speed)
    {
        player=playerObj;
        _navMesh= GetComponent<NavMeshAgent>();
        _navMesh.speed=speed;
    }

    public void PushEnemy()
    {
        _canMove=true;
        _navMesh.isStopped = false;
    }

    public void StopEnemy()
    {
        _canMove=false;
        _navMesh.isStopped = true;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopEnemy();
        }
    }
}
