using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float secondsPerShot; // Cannot Init with '1 / shotsPerSecond'
    private float shootEventTime;

    // Start is called before the first frame update
    public override void Start()
    {
        // Init secondsPerShot
        secondsPerShot = 1 / shotsPerSecond;
        shootEventTime = Time.time + secondsPerShot;

        // Call Parent Start()
        base.Start();
    }

    // Update is called once per frame
    public override void Update() 
    {
        // Call Parent Update()
        base.Update();
    }

    // Movement
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    // Shooting
    public override void Shoot()
    {
        // Update our Timer
        if (Time.time >= shootEventTime)
        {
            Debug.Log("Shots Fired!");
            shootEventTime = Time.time + secondsPerShot;
        }
    }
}
