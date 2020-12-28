﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyController2D controller;
    public float speed;

    private int hp = 3;

    void FixedUpdate()
    {
        controller.Move(speed * Time.fixedDeltaTime, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            speed = speed * -1f;
        }
        else if (collision.gameObject.tag == "Jump")
        {
            controller.Move(0, true);
        }
        else if (collision.gameObject.tag == "ThrowableLight")
        {
            hp -= 1;
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }   
}
