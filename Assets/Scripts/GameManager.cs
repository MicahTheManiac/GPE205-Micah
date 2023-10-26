using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;

    // List to hold out Players
    public List<PlayerController> players;
    public List<AIController> enemies;

    // Called before Start() can run
    private void Awake()
    {
        // Check to see if Instance doesn't exist
        if (instance == null)
        {
            // This is the instance
            instance = this;
            // Don't destroy in new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // There is another instance, destory this
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        // Spawn Player Controller at (0, 0, 0)
        GameObject newPlayerObject = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the Pawn and Connect to Controller
        GameObject newPawnObject = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get Player Controller and Pawn Components
        Controller newController = newPlayerObject.GetComponent<Controller>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();

        // Hook them up!
        newController.pawn = newPawn;
    }
}
