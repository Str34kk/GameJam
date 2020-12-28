using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntyFall : MonoBehaviour
{
    public UnityEngine.Experimental.Rendering.Universal.Light2D playerLightRadius;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThrowableLight")
        {
            playerLightRadius.pointLightOuterRadius += 0.2f;
        }
    }
}
