using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{

    public float damageDone;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Collision Detection
    public void OnTriggerEnter(Collider other)
    {
        // Get Health Comp. from Other Game Obj. that has Collider we are overlapping
        Health otherHealth = other.gameObject.GetComponent<Health>();

        // Only Damage if other has Health Comp.
        if (otherHealth != null)
        {
            // Do Damage
            otherHealth.TakeDamage(damageDone, owner);
        }

        // Destroy even if we didn't do Damage
        Destroy(gameObject);
    }
}
