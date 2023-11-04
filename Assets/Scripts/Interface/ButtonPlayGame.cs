using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayGame : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenu();
        }
    }

    public void QuitGame()
    {
        // Quit a Built App
        Application.Quit();

        // Quit in the Editor
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
