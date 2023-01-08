using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    int isIdle;
    public bool isIdling;
    public bool isWalking;
    private bool hasReachedPlayer = false;
    public float zombieSpeed = 3f;
    public float gravity = 1f;
    float timeOfLastAttack = 0;
    float yRotation;
    Vector3 velocity;
    ZombieStats zombieStats;
    GameObject playerToFollow;
    Animator zombieAnimator;
    NavMeshAgent navMeshAgent;
    RaycastHit[] hit;
    LayerMask layerMask;
    Vector3 rootMotionZombie;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponentInChildren<Animator>();
        zombieAnimator.SetFloat("Speed",0f);
        isIdle = Random.Range(0,2);
        zombieStats = GetComponent<ZombieStats>();

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
        zombieAnimator.SetFloat("Speed",1f,0.3f,Time.deltaTime);
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
