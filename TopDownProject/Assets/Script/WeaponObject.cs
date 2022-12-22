using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapon")]
public class WeaponObject : ScriptableObject
{
    public string weaponName;
    public GameObject weaponObject;
    public Transform weaponCanon;
    public int magazineSize;
    public int numMagazine;
    public int weaponDamage;
    public int fireRate;
}
