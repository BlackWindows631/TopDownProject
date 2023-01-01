using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    PlayerMovement playerMovement;
    public CharacterController characterController;

    Vector3 rootMotion;

    PlayerStates playerCurrentState;

    public enum PlayerStates{
        PISTOL,
        MOVEMENT,
        JOG
    }

    PlayerStates currentState{
        set{
            playerCurrentState = value;

            switch(playerCurrentState){
                case PlayerStates.MOVEMENT:
                animator.Play("Movement");
                break;
                case PlayerStates.JOG:
                animator.Play("Jog");
                break;
                case PlayerStates.PISTOL:
                animator.Play("Pistol");
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        animator.SetFloat("forward",z);
        animator.SetFloat("strafe",x);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("isAiming",true);
        }
        else
        {
            animator.SetBool("isAiming",false);
        }
    }

    private void FixedUpdate() 
    {
        characterController.Move(rootMotion);
        rootMotion = Vector3.zero;

    }

    private void OnAnimatorMove() 
    {
        rootMotion += animator.deltaPosition;
    }
}
