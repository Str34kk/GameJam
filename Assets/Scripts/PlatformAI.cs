using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAI : MonoBehaviour
{
    public float goRight;
    public float goTop;
    public float horizontalSpeed;
    public float verticalSpeed;

    private Vector3 velocity = Vector3.zero;
    private Vector3 awakePosition = Vector3.zero;

    void Awake()
    {
        awakePosition = transform.position;
    }
    void FixedUpdate()
    {
        velocity = new Vector3(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime, 0);
        transform.position += velocity;

        if(transform.position.x >= (awakePosition.x + goRight) || transform.position.x < awakePosition.x)
        {
            horizontalSpeed *= -1;
        }
        if (transform.position.y >= (awakePosition.y + goTop) || transform.position.y < awakePosition.y)
        {
            verticalSpeed *= -1;
        }
    }
}
