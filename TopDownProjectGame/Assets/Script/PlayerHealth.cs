using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 1000;
    public float currentHealth;
    [SerializeField] Slider healthSlider;


    private void Awake() 
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    private void Update() 
    {
        healthSlider.value = currentHealth;
    }
}
