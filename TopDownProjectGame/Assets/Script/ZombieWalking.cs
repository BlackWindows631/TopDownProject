using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWalking : MonoBehaviour
{
    private bool hasReachedPlayer = false;
    public bool isRunning = false;
    float timeOfLastAttack = 0;
    ZombieStats zombieStats;
    GameObject playerToFollow;
    Animator zombieAnimator;
    NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponentInChildren<Animator>();
        zombieAnimator.SetFloat("Speed",0f);
        zombieStats = GetComponent<ZombieStats>();
    }

    void Update()
    {
        Collider[] hitInfo = Physics.OverlapSphere(transform.position,7.5f);

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
        if(isRunning)
        {
            zombieAnimator.SetFloat("Speed",2f,0.3f,Time.deltaTime);
        }
        else
        {
            zombieAnimator.SetFloat("Speed",1f,0.3f,Time.deltaTime);
        }
        HandleZombieRotation();

        float distanceToPlayer = Vector3.Distance(playerToFollow.transform.position,transform.position);
        if(distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            zombieAnimator.SetFloat("Speed",0f);

            if(!hasReachedPlayer)
            {
                hasReachedPlayer = true;
                timeOfLastAttack = Time.time;
            }

            if(Time.time >= timeOfLastAttack + zombieStats.attackSpeed)
            {
                timeOfLastAttack = Time.time;
                PlayerHealth playerHealth = playerToFollow.GetComponent<PlayerHealth>();
                AttackTarget(playerHealth);
            }
        }
        else
        {
            if(hasReachedPlayer)
            {
                hasReachedPlayer = false;
            }
        }
    }

    private void HandleZombieRotation()
    {
        Vector3 direction = playerToFollow.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = rotation;
    }

    private void AttackTarget(PlayerHealth playerToDamage)
    {
        zombieAnimator.SetTrigger("attack");
        zombieStats.DealDamage(playerToDamage);
    }
}
