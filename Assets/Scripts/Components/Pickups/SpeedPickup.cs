using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public HealthPowerup powerup;

    public void OnTriggerEnter(Collider other)
    {
        // Var. to store the other Obj's PowerupManager
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If the other Obj. has a PowerupManager
        if (powerupManager != null)
        {
            // Add the Powerup
            powerupManager.Add(powerup);

            // Destroy the Pickup
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
    }
}
