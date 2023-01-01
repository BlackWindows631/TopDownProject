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
    public TwoBoneIKConstraint rightHandIK;
    public TwoBoneIKConstraint leftHandIK;
    public RigBuilder rigBuilder;

    public void SetupWeapon(int index)
    {
        for(int i = 0 ; i < 28 ; i++)
        {
            weaponList[i].SetActive(false);
        }
        currentWeapon = weaponList[index].GetComponent<WeaponObject>();
        weaponList[index].SetActive(true);
        rightHandIK.data.target = currentWeapon.rightHandIK;
        leftHandIK.data.target = currentWeapon.leftHandIK;
        rigBuilder.Clear();
        rigBuilder.Build();
    }
}
