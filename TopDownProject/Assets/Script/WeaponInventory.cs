using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponInventory : MonoBehaviour
{
    public GameObject[] weaponList;
    RaycastHit hit;
    
    public TextMeshProUGUI textMeshH;

    private void Awake() 
    {
        SetupWeapon(0);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon detected");
            textMeshH.text = "Pick up " + other.gameObject.name;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.tag == "Weapon")
        {
            if(Input.GetKey(KeyCode.E))
            {
                textMeshH.text = "";
                WeaponObject weaponObject = other.gameObject.GetComponent<WeaponObject>();
                SetupWeapon(weaponObject.index);
                Destroy(other.gameObject);
                Debug.Log(other.gameObject.name + " picked up");
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Weapon")
        {
            Debug.Log("Weapon left");
            textMeshH.text = "";
        }
    }

    private void SetupWeapon(int index)
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
        weaponList[index].SetActive(true);
    }
}