using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public string weaponName;
    public Transform weaponCanon;
    WeaponHandler weaponHandler;
    public Transform rightHandIK;
    public Transform leftHandIK;
    
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    public int index;

    private void Awake() 
    {
        weaponHandler = GetComponentInParent<WeaponHandler>();
        weaponName = this.name;
        weaponCanon = this.gameObject.transform.Find("Canon");
    }

    private void OnEnable() 
    {
        weaponHandler.bulletsLeft = magazineSize;
    }
}
