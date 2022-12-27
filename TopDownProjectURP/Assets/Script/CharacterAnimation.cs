using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
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
}
