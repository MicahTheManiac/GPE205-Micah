using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Health Variables
    public float currentHealth;
    public float maxHealth;
    public bool allowOverhealing = false;

    // Indicator
    public Image image;

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

        // Update UI
        CalculateImageFill();
    }

    // Die
    public void Die(Pawn source)
    {
        // Add to Killer's Score
        PlayerController sourceController = source.controller.GetComponent<PlayerController>();
        if (sourceController != null)
        {
            sourceController.AddToScore(100);
        }

        // Destroy this Object
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

        // Update UI
        CalculateImageFill();
    }

    // Calculate Fill Percentage
    private void CalculateImageFill()
    {
        // Perform Calculation and Clamp
        float percentHealth = currentHealth / maxHealth;
        percentHealth = Mathf.Clamp(percentHealth, 0, 1);

        // Set Fill
        image.fillAmount = percentHealth;

    }
}
