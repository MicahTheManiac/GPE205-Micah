using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> powerups;
    private List<Powerup> removedPowerupQueue;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupQueue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        DecrementPowerupTimers();
    }

    // Call LateUpdate()
    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }

    // Add Function to Add Powerup
    public void Add(Powerup powerupToAdd)
    {
        // Apply the Powerup
        powerupToAdd.Apply(this);

        // Add it to the List
        powerups.Add(powerupToAdd);
    }

    // Remove Function to Remove Powerup
    public void Remove(Powerup powerupToRemove)
    {
        // Remove the Powerup
        powerupToRemove.Remove(this);

        // Add it to the Queue
        removedPowerupQueue.Add(powerupToRemove);
    }

    // Powerup Timers
    public void DecrementPowerupTimers()
    {
        // One at a Time
        foreach (Powerup powerup in powerups)
        {
            // Subtract the time it took to draw the frame from Duration
            if (!powerup.isPermanent)
            {
                powerup.duration -= Time.deltaTime;
            }

            // If time is up, Remove Powerup
            if (powerup.duration <= 0)
            {
                Remove(powerup);
            }
        }
    }

    private void ApplyRemovePowerupsQueue()
    {
        // Since we aren't iterating, Remove the Powerups that are in our Queue
        foreach (Powerup powerup in removedPowerupQueue)
        {
            powerups.Remove(powerup);
        }

        // Reset our Temp. Queue List
        removedPowerupQueue.Clear();
    }
}
