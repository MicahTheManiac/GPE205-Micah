using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public bool doRepeatSpawns = false;
    public float spawnDelay = 20.0f;

    private float nextSpawnTime;
    private GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the Pickup (And Init the Timer)
        Spawn(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we Should Repeat this Spawn
        if (doRepeatSpawns)
        {
            // If it is there and nothing spawns
            if (spawnedObject == null)
            {
                // And if it is time to Spawn a Pickup
                if (Time.time > nextSpawnTime)
                {
                    // Spawn the Object
                    Spawn(transform.position);
                }
            }
            else
            {
                // The Object still exists, postpone the spawn
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
    }

    // Function SpawnPickup()
    public void Spawn(Vector3 position)
    {
        GameObject objectToSpawn = objectsToSpawn[UnityEngine.Random.Range(0, objectsToSpawn.Length)];

        // Spawn it and Set the Next time
        spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity) as GameObject;
        nextSpawnTime = Time.time + spawnDelay;
    }
}
