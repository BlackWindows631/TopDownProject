using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Andtech.ProTracer;

public class WeaponHandler : MonoBehaviour
{
    public WeaponInventory weaponInventory;
    public WeaponObject weaponObject;
    public Camera cameraPlayer;
    [SerializeField]
    Bullet bulletPrefab;

    Quaternion bloodSplashRotation;

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

    public ParticleSystem bloodSplash;

    public LayerMask ignoreMeShoot;
    Vector3 mousePosition;
    CharacterAnimation characterAnimation;
    private void Awake() 
    {
        readyToShoot = true;
        weaponInventory = GetComponentInChildren<WeaponInventory>();
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    private void Update() 
    {
        if(weaponInventory.currentWeapon == null)
        {

        }
        else
        {
            weaponObject = weaponInventory.currentWeapon; 
            attackPoint = weaponObject.weaponCanon.transform;
        
            MyInput();
            RotateWeapon();
            text.text = bulletsLeft.ToString() + " / " + weaponObject.magazineSize.ToString();
        }
        
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

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0 && characterAnimation.canShoot)
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

                   

            float x = Random.Range(-weaponObject.spread, weaponObject.spread);
            float y = Random.Range(-weaponObject.spread, weaponObject.spread);

            Vector3 direction = weaponInventory.currentWeapon.weaponCanon.transform.forward + new Vector3(x,y,0);
        
            for(int i = 0 ; i < weaponObject.bulletsPerTap ; i++)
            {
           
                /*var tracer = Instantiate(bulletTrail,attackPoint.position,Quaternion.identity);
                tracer.AddPosition(attackPoint.position); */
                Bullet bullet = Instantiate(bulletPrefab,attackPoint.position,Quaternion.identity);

                bullet.Completed += OnCompleted;

                if(Physics.Raycast(attackPoint.position,direction, out rayHit, weaponObject.range,~ignoreMeShoot))
                {
                    //tracer.transform.position = rayHit.point;
                    bullet.DrawLine(attackPoint.position,rayHit.point,100f,0);

                    if(rayHit.collider.CompareTag("Enemy"))
                    {
                        EnemySystem enemySystem = rayHit.collider.gameObject.GetComponent<EnemySystem>();
                        Instantiate(bloodSplash,rayHit.point,Quaternion.LookRotation(rayHit.normal));
                        enemySystem.health -= weaponObject.damage;
                        Debug.DrawLine(attackPoint.position,rayHit.point,Color.green,1000f);
                    } 
                    else if (rayHit.collider.CompareTag("Barrel"))
                    {
                        ExplosiveBarrel explosiveBarrel = rayHit.collider.gameObject.GetComponent<ExplosiveBarrel>();
                        explosiveBarrel.ExplodeBarrel();
                    } 
                    else
                    {
                        Debug.DrawLine(attackPoint.position,rayHit.point,Color.red,1000f);
                        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0,180,0));
                    }
                }
                else
                {
                    bullet.DrawRay(attackPoint.position,transform.forward,100f,1000f,0f);
                }

            }
        
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
            bulletsLeft--;
            bulletsShot--;
            Invoke("ResetShot",weaponObject.timeBetweenShooting);

            if(bulletsShot > 0 && bulletsLeft > 0){
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

    private void RotateWeapon()
    {
        /*mousePosition = Input.mousePosition;
        Ray ray = cameraPlayer.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hitInfo, float.MaxValue, 1 << LayerMask.NameToLayer("Mousable")))
        {
            var target = hitInfo.point;
            target.y = weaponInventory.currentWeapon.transform.position.y;
            weaponInventory.currentWeapon.transform.LookAt(target);
            
        }*/
    }

    private void OnCompleted(object sender, System.EventArgs e)
    {
        if(sender is TracerObject tracerObject)
        {
            Destroy(tracerObject.gameObject);
        }
    }
}
