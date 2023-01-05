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
    float yRotation;
    Vector3 velocity;
    Transform playerToFollow;
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

    private void HandleZombieMovement()
    {
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
        Vector3 targetPosition = new Vector3(playerToFollow.transform.position.x, transform.position.y, playerToFollow.transform.position.z);
        transform.LookAt(targetPosition);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,7.5f);
    }
}
