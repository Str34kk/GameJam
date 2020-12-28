using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
    public void btnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void btnCredits()
    {
        SceneManager.LoadScene(2);
    }

    public void btnExit()
    {
        Application.Quit();
    }
}
