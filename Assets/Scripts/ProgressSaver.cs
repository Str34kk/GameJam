using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressSaver : MonoBehaviour
{
    public int Level;

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
        }
    }
}