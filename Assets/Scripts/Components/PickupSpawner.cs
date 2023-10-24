using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;

    private float nextSpawnTime;
    private Transform tf;
    private GameObject spawnedPickup;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
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
                // Spawn it and Set the Next time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // The Object still exists, postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
