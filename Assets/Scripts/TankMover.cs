using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // Hold our Rigidbody
    private Rigidbody rb;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get Rigidbody Component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        // rb.MovePosition(transform.position + moveVector);
        transform.position += moveVector;
    }

    public override void Rotate(float speed)
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
