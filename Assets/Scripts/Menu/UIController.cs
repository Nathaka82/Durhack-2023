using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public ScreenTransition ST;
    public void LoadGame()
    {
        ST.LoadLevel(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadCredits()
    {
        ST.LoadLevel(2);
    }
}
