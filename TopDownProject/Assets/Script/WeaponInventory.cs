using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public GameObject[] weaponList;
    RaycastHit hit;


    private void Awake() 
    {
        DisableAllWeapon();
        ActiveWeapon(0);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon detected");
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon left");
        }
    }

    private void DisableAllWeapon()
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
    }

    private void ActiveWeapon(int i)
    {
        weaponList[i].SetActive(true);
    }
}
