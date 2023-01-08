using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [SerializeField] private float damage;
    public float attackSpeed;
    [SerializeField] private bool canAttack;
    public float health;
    float maxHealth;

    private void Awake() 
    {
        InitZombieStats();
    }

    private void Update() 
    {
        HandleDeath();
    }

    public void DealDamage(PlayerHealth playerHealth)
    {
        playerHealth.currentHealth = playerHealth.currentHealth - damage;
    }

    public void InitZombieStats()
    {
        health = 100;
        maxHealth = health;

        damage = 10;
        attackSpeed = 1.5f;
        canAttack = true;
    }

    private void HandleDeath()
    {
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
