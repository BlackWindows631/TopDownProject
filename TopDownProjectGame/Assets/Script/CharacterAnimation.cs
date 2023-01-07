using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    WeaponHandler weaponHandler;
    WeaponInventory weaponInventory;

    [SerializeField]TwoBoneIKConstraint rightHandIK;
    [SerializeField]TwoBoneIKConstraint leftHandIK;
    Rig rig;

    [SerializeField]
    PlayerMovement playerMovement;
    public CharacterController characterController;
    Vector3 velocity;
    public bool isPrimaryEquipped = false;
    public bool isSecondaryEquipped = false;
    public bool canShoot;

    Vector3 moveVelocity;
    float gravity = -20f;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        weaponHandler = GetComponent<WeaponHandler>();
        weaponInventory = GetComponentInChildren<WeaponInventory>();
        rig = GetComponentInChildren<Rig>();
    }

    // Update is called once per frame
    void Update()
    {   
        float z = Input.GetAxis("Vertical");

        animator.SetFloat("forward",z);

        if(animator.GetBool("isRunning"))
        {
            playerMovement.speed = 7f;
            canShoot = false;
        }
        else
        {
            playerMovement.speed = 3.5f;
            canShoot = true;
        }

        if(Input.GetKey(KeyCode.LeftShift) && animator.GetFloat("forward") > 0 && playerMovement.canRun)
        {
            animator.SetBool("isRunning", true);
            canShoot = false;
        }
        else
        {
            animator.SetBool("isRunning", false);
            canShoot = true;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            animator.Play("Roll");
        }
        
        if(animator.GetBool("isRunning") && animator.GetFloat("forward") < 0)
        {
            playerMovement.speed = 3.5f;
            animator.SetBool("isRunning",false);
        }

        if(weaponInventory.secondaryWeapon != null)
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                isSecondaryEquipped = !isSecondaryEquipped;
                if(isSecondaryEquipped == true && isPrimaryEquipped == false)
                {
                    weaponInventory.ActivateSecondaryWeapon();
                    animator.SetBool("hasGun",true);
                }
                else if(isSecondaryEquipped == false && isPrimaryEquipped == false)
                {
                    weaponInventory.DeactivateSecondaryWeapon();
                    animator.SetBool("hasGun",false);
                    weaponInventory.currentWeapon = null;
                }
            }
        }

        if(weaponInventory.primaryWeapon != null)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                isPrimaryEquipped = !isPrimaryEquipped;
                if(isPrimaryEquipped == true && isSecondaryEquipped == false)
                {
                    weaponInventory.ActivatePrimaryWeapon();
                    animator.SetBool("hasWeapon",true);
                }
                else if(isPrimaryEquipped == false && isSecondaryEquipped == false)
                {
                    weaponInventory.DeactivatePrimaryWeapon();
                    animator.SetBool("hasWeapon",false);
                    weaponInventory.currentWeapon = null;
                }
            }
        }

        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
    }

}
