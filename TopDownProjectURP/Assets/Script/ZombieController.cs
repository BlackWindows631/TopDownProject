using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    int isIdle;
    public bool isIdling;
    public bool isWalking;
    public float zombieSpeed = 3f;
    public float gravity = 1f;
    Vector3 velocity;
    Transform playerToFollow;
    CharacterController zombieController;
    Animator zombieAnimator;
    NavMeshAgent navMeshAgent;
    RaycastHit[] hit;
    LayerMask layerMask;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponentInChildren<Animator>();
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
        Collider[] hitInfo = Physics.OverlapSphere(transform.position,7.5f);

        foreach (Collider hit in hitInfo)
        {
            if(hit.gameObject.CompareTag("Player"))
            {
                playerToFollow = hit.gameObject.transform;
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

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerToFollow = other.gameObject.transform;
        }
    }

    private void HandleZombieMovement()
    {
        navMeshAgent.SetDestination(playerToFollow.position);
        zombieAnimator.SetFloat("Speed",1f,0.3f,Time.deltaTime);
        HandleZombieRotation();

        float distanceToPlayer = Vector3.Distance(playerToFollow.position,transform.position);
        if(distanceToPlayer <= navMeshAgent.stoppingDistance)
        {
            zombieAnimator.SetFloat("Speed",0f);
        }
    }

    private void HandleZombieRotation()
    {
        Vector3 direction = playerToFollow.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.
        
        rotation = rotation;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,7.5f);
    }
}
