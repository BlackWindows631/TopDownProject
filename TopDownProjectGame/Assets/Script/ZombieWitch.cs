using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWitch : MonoBehaviour
{
    GameObject playerToFollow;
    Animator zombieAnimator;
    NavMeshAgent navMeshAgent;
    bool hasReachedPlayer = false;
    float timeOfLastAttack;
    ZombieStats zombieStats;

    private void Awake() 
    {
        zombieAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>(); 
        zombieStats = GetComponent<ZombieStats>();
    }

    private void Update() 
    {
        Collider[] hitInfo = Physics.OverlapSphere(transform.position,10f);

        foreach (Collider hit in hitInfo)
        {
            if(hit.gameObject.CompareTag("Player"))
            {
                playerToFollow = hit.gameObject;
            }
        }

        if(playerToFollow == null)
        {
            
        }
        else
        {
            HandleZombieMovement();
        }

    }

    private void HandleZombieMovement()
    {
        zombieAnimator.SetFloat("Speed",1f,0.3f,Time.deltaTime);
        HandleZombieRotation();

        float distanceToPlayer = Vector3.Distance(playerToFollow.transform.position,transform.position);
        if(distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            zombieAnimator.SetFloat("Speed",0f);

            if(!hasReachedPlayer)
            {
                hasReachedPlayer = true;
            }

            if(Time.time >= timeOfLastAttack + zombieStats.attackSpeed)
            {
                timeOfLastAttack = Time.time;
                AttackPlayer();
            }
        }

        if(distanceToPlayer < 5f)
        {
            zombieAnimator.SetFloat("Speed",-1f);
        }
    }

    private void HandleZombieRotation()
    {
        Vector3 direction = playerToFollow.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = rotation;
    }

    private void AttackPlayer()
    {
        zombieAnimator.SetTrigger("fire");
    }
}
