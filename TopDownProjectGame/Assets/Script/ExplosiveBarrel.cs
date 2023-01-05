using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public ParticleSystem explosion;

    public ParticleSystem fireExplosion;

    public void ExplodeBarrel()
    {
        Instantiate(explosion,transform.position,Quaternion.identity);
        Instantiate(fireExplosion,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
