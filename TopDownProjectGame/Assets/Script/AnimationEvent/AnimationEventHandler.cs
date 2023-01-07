using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    Rigidbody playerRigidbody;
    PlayerMovement playerMovement;
    Animator animator;

    private void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();    
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void StopRotation()
    {
        playerMovement.handleRotation = false;
    }

    void StartRotation()
    {
        playerMovement.handleRotation = true;
    }

    void StopPosition()
    {
        playerMovement.handlePosition = false;
    }

    void StartPosition()
    {
        playerMovement.handlePosition = true;
    }

    void StartRootMotion()
    {
        animator.applyRootMotion = true;
    }

    void StopRootMotion()
    {
        animator.applyRootMotion = false;
    }
}
