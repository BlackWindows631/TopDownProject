using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public string weaponName;
    public Transform weaponCanon;
    public int magazineSize;
    public int numMagazine;
    public int weaponDamage;
    public int fireRate;
    public int index;

    private void Awake() 
    {
        weaponName = this.name;
        weaponCanon = this.gameObject.transform.Find("Canon");
    }
}
