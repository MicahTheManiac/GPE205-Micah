using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeedPowerup : Powerup
{
    public float speedToAdd;
    public float turnSpeedMultiplier = 2.0f;
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            // Apply speedToAdd to Movement Speed
            targetPawn.moveSpeed += speedToAdd;

            // Apply speedToAdd and Multiply since we are Turning
            targetPawn.turnSpeed += (speedToAdd * turnSpeedMultiplier);
        }
    }

    public override void Remove(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            // Remove speedToAdd to Movement Speed
            targetPawn.moveSpeed -= speedToAdd;

            // Remove speedToAdd and its Multiplier
            targetPawn.turnSpeed -= (speedToAdd * turnSpeedMultiplier);
        }
    }
}
