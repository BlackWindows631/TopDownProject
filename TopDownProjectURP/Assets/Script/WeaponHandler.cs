using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHandler : MonoBehaviour
{
    public WeaponInventory weaponInventory;
    public WeaponObject weaponObject;

    [Header("Gun stats")]
    public int bulletsLeft, bulletsShot;

    [Header("Bools")]
    bool shooting,readyToShoot,reloading;

    [Header("References")]
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    [Header("Graphics")]
    public GameObject muzzleFlash,bulletHoleGraphic;
    public TextMeshProUGUI text;
    public TrailRenderer bulletTrail;

    private void Awake() 
    {
        readyToShoot = true;
        weaponInventory = GetComponentInChildren<WeaponInventory>();
    }

    private void Update() 
    {
        weaponObject = weaponInventory.currentWeapon;
        attackPoint = weaponObject.weaponCanon.transform;
        MyInput();
        text.text = bulletsLeft.ToString() + " / " + weaponObject.magazineSize.ToString();
    }

    private void MyInput()
    {
        if(weaponObject.allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else 
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < weaponObject.magazineSize && !reloading)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = weaponObject.bulletsPerTap;
            Shoot();
        }
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished",weaponObject.reloadTime);
    }

    private void Shoot()
    {
        readyToShoot = false;

        var tracer = Instantiate(bulletTrail,attackPoint.position,Quaternion.identity);
        tracer.AddPosition(attackPoint.position);

        float x = Random.Range(-weaponObject.spread, weaponObject.spread);
        float y = Random.Range(-weaponObject.spread, weaponObject.spread);

        Vector3 direction = weaponInventory.currentWeapon.weaponCanon.transform.forward + new Vector3(x,y,0);

        Debug.DrawRay(attackPoint.position,direction,Color.red,weaponObject.range);
        if(Physics.Raycast(attackPoint.position,direction, out rayHit, weaponObject.range))
        {
            tracer.transform.position = rayHit.point;
            Debug.Log(rayHit.collider.name);
            Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0,180,0));
            if(rayHit.collider.CompareTag("Enemy"))
            {
                // Damage enemy
            }
        }

        
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot",weaponObject.timeBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot",weaponObject.timeBetweenShots);
        }
        
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void ReloadFinished()
    {
        bulletsLeft = weaponObject.magazineSize;
        reloading = false;
    }
}
