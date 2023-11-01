using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScorePowerup : Powerup
{
    public int scoreToAdd;

    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        PlayerController targetController = targetPawn.controller.GetComponent<PlayerController>();
        if (targetController != null)
        {
            targetController.AddToScore(scoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Do Nothing
    }
}
