using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Prefabs
    public GameObject playerControllerPrefab;
    public bool isMultiplayerMode = false;
    public GameObject playerTwoControllerPrefab;
    public GameObject tankPawnPrefab;

    // Lists
    public List<Transform> playerSpawnTransforms;
    public List<GameObject> scoreTextPassthrough;

    // List to hold our Controllers
    public List<PlayerController> players;
    public List<AIController> enemies;

    // Game States
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;

    // Game Modes
    public GameObject SingleplayerGameMode;
    public GameObject MultiplayerGameMode;

    // Game Over Screens
    public Canvas MultiplayerGameOverCanvasOne;
    public Canvas MultiplayerGameOverCanvasTwo;

    // Called before Start() can run
    private void Awake()
    {
        // Check to see if Instance doesn't exist
        if (instance == null)
        {
            // This is the instance
            instance = this;
            // -- -- SINCE OUR GAME IS IN ONE SCENE WE CAN IGNORE DON'T DESTROY ON LOAD -- -- (Causes Issues when Restarting from GameOver)
            // Don't destroy in new scene
            // DontDestroyOnLoad(gameObject);
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
        ActivateTitleScreen();
    }

    // Spawn Player
    public void SpawnPlayer(GameObject controllerPrefab, Transform spawnPoint)
    {
        // Spawn Player Controller at (0, 0, 0)
        GameObject newPlayerObject = Instantiate(controllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the Pawn and Connect to Controller
        GameObject newPawnObject = Instantiate(tankPawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;

        // Get Player Controller and Pawn Components
        Controller newController = newPlayerObject.GetComponent<Controller>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();

        // Hook them up!
        newController.pawn = newPawn;
    }

    // Deactivate All States
    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    // Activate the Title Screen
    public void ActivateTitleScreen()
    {
        // Deactivate all States
        DeactivateAllStates();

        // Activate the Title Screen
        TitleScreenStateObject.SetActive(true);
        Debug.Log("Game is in TITLE SCREEN State");
    }

    // Activate the Main Menu
    public void ActivateMainMenu()
    {
        // Deactivate all States
        DeactivateAllStates();

        // Activate the Main Menu
        MainMenuStateObject.SetActive(true);
        Debug.Log("Game is in MAIN MENU State");
    }

    // Activate the Options Screen
    public void ActivateOptionsScreen()
    {
        // Deactivate all States
        DeactivateAllStates();

        // Activate the Options Screen
        OptionsScreenStateObject.SetActive(true);
        Debug.Log("Game is in OPTIONS SCREEN State");
    }

    // Activate the Credits Screen
    public void ActivateCreditsScreen()
    {
        // Deactivate all States
        DeactivateAllStates();

        // Activate the Credits Screen
        CreditsScreenStateObject.SetActive(true);
        Debug.Log("Game is in CREDITS SCREEN State");
    }

    // Activate the Gameplay State
    public void ActivateGameplay()
    {
        // Deactivate all States
        DeactivateAllStates();

        // Activate the Gameplay State
        GameplayStateObject.SetActive(true);

        // Check to see What Gamemode (SP or MP)
        if (!isMultiplayerMode)
        {
            // Singleplayer
            ActivateSingleplayerMode();
        }
        else
        {
            // Multiplayer
            ActivateMultiplayerMode();
        }
        
        Debug.Log("Game is in GAMEPLAY State");
    }

    // Activate the Game Over Screen
    public void ActivateGameOverScreen()
    {
        // See if we are in Singleplayer
        if (!isMultiplayerMode)
        {
            // Deactivate all States
            DeactivateAllStates();

            // Activate the Game Over Screen
            GameOverScreenStateObject.SetActive(true);

            Debug.Log("Game is in GAME OVER SCREEN State");
        }
        else
        {
            
        }
    }

    public void ActivateSingleplayerMode()
    {
        // Activate Singleplayer
        SingleplayerGameMode.SetActive(true);

        // Deactivate Multiplayer
        MultiplayerGameMode.SetActive(false);

        // Spawn our Player
        SpawnPlayer(playerControllerPrefab, playerSpawnTransforms[0]);
    }

    public void ActivateMultiplayerMode()
    {
        // Deactivate Singleplayer
        SingleplayerGameMode.SetActive(false);

        // Activate Multiplayer
        MultiplayerGameMode.SetActive(true);

        // Spawn Player 1
        SpawnPlayer(playerControllerPrefab, playerSpawnTransforms[0]);

        // Spawn Player 2
        SpawnPlayer(playerTwoControllerPrefab, playerSpawnTransforms[1]);
    }
}