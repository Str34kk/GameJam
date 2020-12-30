using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSaver : MonoBehaviour
{
    public int Level;

    void Awake()
    {
        PlayerPrefs.SetInt("1", 1);
        Level += 1;
    }
    void Saver()
    {
        if (Level > 0)
        {
            PlayerPrefs.SetInt(Level.ToString(), 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Saver();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}