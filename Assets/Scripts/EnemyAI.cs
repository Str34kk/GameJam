using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Controller2D controller;
    public float speed;

    void FixedUpdate()
    {
        controller.Move(speed * Time.fixedDeltaTime, false);
        //dash = false;
    }
    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            speed = speed * -1f;
            Flip();
        }
        if (collision.gameObject.tag == "Player")
        {
            speed = speed * -1f;
            Flip();
        }
    }

    
}
