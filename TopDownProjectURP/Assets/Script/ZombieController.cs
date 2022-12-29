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

    void Awake()
    {
        zombieController = GetComponent<CharacterController>();
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
        zombieAnimator.SetBool("isWalking",isWalking);
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
        isWalking = true;
    }
}
