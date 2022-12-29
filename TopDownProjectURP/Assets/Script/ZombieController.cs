using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    int isIdle;
    public bool isIdling;
    Transform playerToFollow;
    public NavMeshAgent zombieMesh;
    void Awake()
    {
        zombieMesh = GetComponent<NavMeshAgent>();

        isIdle = Random.Range(0,2);
        if(isIdle == 1)
        {
            isIdling = true;
        }
        else
        {
            isIdling = false;
        }
    }

    void Update()
    {
        zombieMesh.SetDestination(playerToFollow.position);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerToFollow = other.gameObject.transform;
        }
    }
}
