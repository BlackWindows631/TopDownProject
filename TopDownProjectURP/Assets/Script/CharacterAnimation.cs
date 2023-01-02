using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    WeaponHandler weaponHandler;
    WeaponInventory weaponInventory;

    [SerializeField]
    PlayerMovement playerMovement;
    public CharacterController characterController;
    public bool isPrimaryEquipped = false;
    public bool isSecondaryEquipped = false;
    bool isAimingAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        weaponHandler = GetComponent<WeaponHandler>();
        weaponInventory = GetComponentInChildren<WeaponInventory>();
    }

    // Update is called once per frame
    void Update()
    {   
        isAimingAnimator = animator.GetBool("isAiming");

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

        if(Input.GetKey(KeyCode.Mouse0))
        {
            if(isPrimaryEquipped)
            {
                if(!isAimingAnimator)
                {
                    animator.Play("Rifle_Aim_Center");
                }
            }

            if(isSecondaryEquipped)
            {
                if(!isAimingAnimator)
                {
                    animator.Play("Pistol_Aim_Center");
                }
            }
        }

        if(Input.GetKey(KeyCode.Mouse1))
        {
            animator.SetBool("isAiming",true);
            if(isPrimaryEquipped)
            {
                animator.Play("Rifle_Aim_Center");

            }
            if(isSecondaryEquipped)
            {
                animator.Play("Pistol_Aim_Center");
            }
        }
        else
        {
            animator.SetBool("isAiming",false);
        }

        if(weaponInventory.secondaryWeapon != null)
        {
            if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                isSecondaryEquipped = !isSecondaryEquipped;
                if(isSecondaryEquipped == true && isPrimaryEquipped  == true)
                {
                    isPrimaryEquipped = !isPrimaryEquipped;
                    animator.Play("Pistol_Unholster");
                    weaponInventory.DeactivatePrimaryWeapon();
                    weaponInventory.ActivateSecondaryWeapon();
                }
                else if(isSecondaryEquipped == true && isPrimaryEquipped == false)
                {
                    animator.Play("Pistol_Unholster");
                    weaponInventory.ActivateSecondaryWeapon();
                }
                else if(isSecondaryEquipped == false && isPrimaryEquipped == false)
                {
                    animator.Play("Pistol_Holster");
                    weaponInventory.DeactivateSecondaryWeapon();
                    weaponInventory.currentWeapon = null;
                }
                animator.SetBool("isSecondary",isSecondaryEquipped);
                animator.SetBool("isPrimary",isPrimaryEquipped);
            }
        }

        if(weaponInventory.primaryWeapon != null)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                isPrimaryEquipped = !isPrimaryEquipped;
                if(isPrimaryEquipped == true && isSecondaryEquipped == true)
                {
                    isSecondaryEquipped = !isSecondaryEquipped;
                    animator.Play("Rifle_Unholster");
                    weaponInventory.DeactivateSecondaryWeapon();
                    weaponInventory.ActivatePrimaryWeapon();
                }
                else if(isPrimaryEquipped == true && isSecondaryEquipped == false)
                {
                    animator.Play("Rifle_Unholster");
                    weaponInventory.ActivatePrimaryWeapon();
                }
                else if(isPrimaryEquipped == false && isSecondaryEquipped == false)
                {
                    animator.Play("Rifle_Holster");
                    weaponInventory.DeactivatePrimaryWeapon();
                    weaponInventory.currentWeapon = null;
                }
                animator.SetBool("isPrimary", isPrimaryEquipped);
                animator.SetBool("isSecondary",isSecondaryEquipped);
            }
        }
        
    }
}
