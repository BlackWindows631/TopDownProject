using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Animations.Rigging;

public class WeaponInventory : MonoBehaviour
{
    public GameObject[] weaponList;
    RaycastHit hit;
    
    public TextMeshProUGUI textMeshH;
    public WeaponObject currentWeapon;

    public WeaponObject primaryWeapon;
    public WeaponObject secondaryWeapon;

    public void SetupPrimaryWeapon(int index)
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
        primaryWeapon = weaponList[index].GetComponent<WeaponObject>();
        weaponList[index].SetActive(true);
    }

    public void SetupSecondaryWeapon(int index)
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
        secondaryWeapon = weaponList[index].GetComponent<WeaponObject>();
        weaponList[index].SetActive(true);
    }


}
