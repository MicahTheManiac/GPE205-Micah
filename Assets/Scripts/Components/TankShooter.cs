using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    // Tank Shooter Shoot Func
    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        // Instantiate our Projectile
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;

        // Get Damage On Hit Comp.
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        // If it has DoH
        if (doh != null)
        {
            // Set damageDone in DoH to value passed in
            doh.damageDone = damageDone;

            // Set the owner to the Pawn that shot this Shell. Otherwise owner is null.
            doh.owner = GetComponent<Pawn>();
        }

        // Get Rigidbody Comp.
        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        // If it has Rigidbody
        if (rb != null)
        {
            // AddForce to move Shell Forward
            rb.AddForce(firepointTransform.forward * fireForce);
        }

        // Destroy after a set time
        Destroy(newShell, lifespan);

    }

}
