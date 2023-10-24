using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        // TODO: Apply Health Changes
    }

    public override void Remove(PowerupManager target)
    {
        // TODO: Remove Health Changes
    }
}
