using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{ 
    public void btnPlay()
    {
        SceneManager.LoadScene("Demo");
    }

    public void btnCredits()
    {
        SceneManager.LoadScene("Demo");
    }

    public void btnExit()
    {
        Application.Quit();
    }
}
