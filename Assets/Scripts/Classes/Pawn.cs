using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Movement Vars.
    public float moveSpeed;
    public float turnSpeed;
    public Mover mover;
    public Controller controller;

    // Shooting Vars.
    public float shotsPerSecond = 2.0f;
    public Shooter shooter;
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // Movement Functions
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();

    // Shoot Function
    public abstract void Shoot();

    // Rotate Towards Function -- Will Mainly be used for AI
    public abstract void RotateTowards(Vector3 targetPosition);

    // Make Noise
    public void MakeNoise(bool ShouldMakeNoise)
    {
        if (gameObject.GetComponent<NoiseMaker>() != null)
        {
            gameObject.GetComponent<NoiseMaker>().makingNoise = ShouldMakeNoise;
        }
    }
}
