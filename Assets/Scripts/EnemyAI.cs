using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyController2D controller;
    public float speed;
    public int hp = 3;
    public bool isBoss = false;
    public BossController bossController;

    void FixedUpdate()
    {
        controller.Move(speed * Time.fixedDeltaTime, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.transform.localScale.x == transform.localScale.x)
        {
            speed = speed * -1f;
        }
        else if (collision.gameObject.tag == "ThrowableLight")
        {
            hp -= 1;
            if (hp <= 0)
            {
                Destroy(gameObject);
                if(isBoss)
                {
                    bossController.OnBossKill();
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            speed = speed * -1f;
        }
        else if (collision.gameObject.tag == "Jump")
        {
            controller.Move(0, true);
        }
    }

}
