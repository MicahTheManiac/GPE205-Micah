using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Health Variables
    public float currentHealth;
    public float maxHealth;
    public bool allowOverhealing = false;

    // Start is called before the first frame update
    void Start()
    {
        // Set Health to Max
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Take Damage
    public void TakeDamage(float amount, Pawn source)
    {
        // Do Damage
        currentHealth = currentHealth - amount;
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);

        // Clamp the Health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log(gameObject.name + ": My Health is " + currentHealth);

        // Check to see if Health is <= 0
        if (currentHealth <= 0)
        {
            Die(source);
            Debug.Log(gameObject.name + ": I should be dead!");
        }
    }

    // Die
    public void Die(Pawn source)
    {
        Destroy(gameObject);
    }

    // Healing
    public void Heal(float amount, Pawn source)
    {
        // Do Healing
        currentHealth += amount;
        Debug.Log(source.name + " did " + amount + " healing to " + gameObject.name);

        // Clamp the Health
        if (!allowOverhealing)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }
    }
}
