using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;

	void Start()
    {
		Destroy(gameObject, 5f);
	}
	void FixedUpdate()
	{
		if (!hasHit)
			GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Destroy(gameObject);
		}
	}
}
