using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerController : Controller
{
    // Define Keys
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Hold our Score & Lives
    public int score = 0;
    public int lives = 3;

    // UI
    public GameObject scoreText;
    private TextMeshProUGUI uiScoreText;

    private bool isMissingPawn;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the Players
            if (GameManager.instance.players != null)
            {
                // Register with GameManager
                GameManager.instance.players.Add(this);
            }

            // Get Score Text Passthrough
            //scoreText = GameManager.instance.scoreTextPassthrough;
            uiScoreText = scoreText.GetComponent<TextMeshProUGUI>();
        }

        // Set Score Text
        uiScoreText.text = ("Score: " + score);

        // Run Parent Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Check for Pawn
        CheckForPawn();

        // Process Keyboard Inputs
        ProcessInputs();

        // Run Parent Update
        base.Update();
    }

    // Process our Inputs
    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        if (Input.GetKey(shootKey))
        {
            pawn.Shoot();
        }
    }

    // Add to Score
    public void AddToScore(int amount)
    {
        score += amount;

        // Update Score Text
        uiScoreText.text = ("Score: " + score);
        Debug.Log("Score is: " + score + "\n Increased by: " + amount);
    }

    // Run Destory Code
    public void OnDestroy()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the Players
            if (GameManager.instance.players != null)
            {
                // Deregister with GameManager
                GameManager.instance.players.Remove(this);
            }
        }
    }

    public void CheckForPawn()
    {
        if (!isMissingPawn)
        {
            if (pawn == null)
            {
                isMissingPawn = true;
                if (GameManager.instance != null)
                {
                    GameManager.instance.ActivateGameOverScreen();
                }
            }
        }
    }

}
