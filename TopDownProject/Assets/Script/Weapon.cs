using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapon")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public GameObject weaponObject;
    public Transform weaponCanon;
    public int magazineSize;
    public int numMagazine;

}
