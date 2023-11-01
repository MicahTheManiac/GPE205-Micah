using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Text scoreText;

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
        }

        // Set Score Text
        scoreText.text = ("Score: " + score);

        // Run Parent Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
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
            pawn.MakeNoise(true);
        }
        else if (!Input.GetKey(moveForwardKey))
        {
            pawn.MakeNoise(false);
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
            pawn.MakeNoise(true);
        }
        else if (!Input.GetKey(moveBackwardKey))
        {
            pawn.MakeNoise(false);
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

        // Set Score Text
        scoreText.text = ("Score: " + score);
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
}
