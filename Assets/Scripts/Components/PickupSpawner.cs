using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;

    private float nextSpawnTime;
    // private Transform tf;
    private GameObject spawnedPickup;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the Pickup (And Init the Timer)
        SpawnPickup(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // If it is there and nothing spawns
        if (spawnedPickup == null)
        {
            // And if it is time to Spawn a Pickup
            if (Time.time > nextSpawnTime)
            {
                // Spawn the Pickup
                SpawnPickup(transform.position);
            }
        }
        else
        {
            // The Object still exists, postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    // Function SpawnPickup()
    public void SpawnPickup(Vector3 position)
    {
        // Spawn it and Set the Next time
        spawnedPickup = Instantiate(pickupPrefab, position, Quaternion.identity) as GameObject;
        nextSpawnTime = Time.time + spawnDelay;
    }
}
