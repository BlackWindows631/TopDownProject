using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public int health;
    private void Awake() 
    {

    }

    private void Update() 
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
