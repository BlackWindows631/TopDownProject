using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public TextMeshProUGUI textMeshH;
    WeaponInventory weaponInventory;
    WeaponIndex weaponIndex;

    private void Awake() 
    {
        weaponInventory = GetComponentInChildren<WeaponInventory>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Primary") || other.gameObject.CompareTag("Secondary"))
        {
            textMeshH.text = "Pick up " + other.gameObject.name;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Primary") || other.gameObject.CompareTag("Secondary"))
        {
            if(Input.GetKey(KeyCode.E))
            {
                if(other.gameObject.CompareTag("Primary"))
                {
                    weaponIndex = other.gameObject.GetComponent<WeaponIndex>();
                    weaponInventory.SetupPrimaryWeapon(weaponIndex.weaponIndex);
                    textMeshH.text = "";
                    Destroy(other.gameObject);
                }
                else if(other.gameObject.CompareTag("Secondary"))
                {
                    weaponIndex = other.gameObject.GetComponent<WeaponIndex>();
                    weaponInventory.SetupSecondaryWeapon(weaponIndex.weaponIndex);
                    textMeshH.text = "";
                    Destroy(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Primary") || other.gameObject.CompareTag("Secondary"))
        {
            textMeshH.text = "";
        }
    }
}
