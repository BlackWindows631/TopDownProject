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

        if(Input.GetKey(KeyCode.LeftShift) && playerMovement.canRun)
        {
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning",false);
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
