using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCredits : MonoBehaviour
{
    public void ChangeToCreditsScreen()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCreditsScreen();
        }
    }
}
