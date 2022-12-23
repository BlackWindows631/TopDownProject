using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;


    private void Awake() 
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "AddHealth")
        {
            if(currentHealth >= 100)
            {
                currentHealth = 100;
            }
            else
            {
                currentHealth += 20;
            }
        }

        if(other.gameObject.tag == "RemoveHealth")
        {
            if(currentHealth <= 14)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= 15;
            }
        }
    }
}
