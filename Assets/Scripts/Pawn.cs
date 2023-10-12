using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Public Variables
    public float moveSpeed;
    public float turnSpeed;
    public Mover mover;

    public float fireRate = 2.0f;
    public float shotsPerSecond = 2.0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
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
}
