using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        // Apply Health changes
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            // The 2nd Param. is the Pawn that Causes Healing. They Healed themselves
            targetHealth.Heal(healthToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Remove Health changes
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            // The 2nd Param. is the Pawn that Causes Damage. They Hurt themselves
            targetHealth.TakeDamage(healthToAdd, target.GetComponent<Pawn>());
        }
    }
}
