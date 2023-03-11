using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void FixedUpdate()
    {   

        
    }


    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetMaxHealth(currentHealth);
    }


}
