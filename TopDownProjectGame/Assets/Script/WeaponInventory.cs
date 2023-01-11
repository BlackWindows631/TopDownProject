using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations.Rigging;

public class WeaponInventory : MonoBehaviour
{
    public GameObject[] weaponList;
    RaycastHit hit;

    public TextMeshProUGUI primaryWeaponText;
    public TextMeshProUGUI secondaryWeaponText;
    
    public TextMeshProUGUI textMeshH;
    public WeaponObject currentWeapon;

    public WeaponObject primaryWeapon;
    public WeaponObject secondaryWeapon;
    GameObject primaryWeaponGameobject;
    GameObject secondaryWeaponGameobject;

    private void Update() 
    {
        if(primaryWeapon == null)
        {
            primaryWeaponText.text = "1. NULL";
        }
        else
        {
            primaryWeaponText.text = "1. " + primaryWeapon.name;
        }
        
        if (secondaryWeapon == null)
        {
            secondaryWeaponText.text = "2. NULL";
        }
        else
        {
            secondaryWeaponText.text = "2. " + secondaryWeapon.name;
        }

        if(primaryWeapon != null)
        {
            primaryWeaponGameobject = primaryWeapon.gameObject;
        }

        if(secondaryWeapon != null)
        {
            secondaryWeaponGameobject = secondaryWeapon.gameObject;
        }

        if(primaryWeapon != null && primaryWeaponGameobject.activeInHierarchy)
        {
            currentWeapon = primaryWeapon;
        }
        else if(secondaryWeapon != null && secondaryWeaponGameobject.activeInHierarchy)
        {
            currentWeapon = secondaryWeapon;
        }

        if(currentWeapon == null)
        {
            textMeshH.text = "XX/XX";
        }
    }

    public void SetupPrimaryWeapon(int index)
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
        primaryWeapon = weaponList[index].GetComponent<WeaponObject>();
    }

    public void SetupSecondaryWeapon(int index)
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
        secondaryWeapon = weaponList[index].GetComponent<WeaponObject>();
    }

    public void ActivatePrimaryWeapon()
    {
        primaryWeaponGameobject.SetActive(true);
    }

    public void DeactivatePrimaryWeapon()
    {
        primaryWeaponGameobject.SetActive(false);
    }

    public void ActivateSecondaryWeapon()
    {
        secondaryWeaponGameobject.SetActive(true);
    }

    public void DeactivateSecondaryWeapon()
    {
        secondaryWeaponGameobject.SetActive(false);
    }

}
