using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class Throwable : MonoBehaviour
{
	public Vector2 direction;
	public bool hasHit = false;
	public float speed = 10f;
	public Light2D lightRadius;

	void FixedUpdate()
	{
		if (!hasHit)
			GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy")
		{
			StartCoroutine(DestroyLight());
		}
		else if (collision.gameObject.tag == "Player")
		{
			Destroy(gameObject);
		}
	}

	IEnumerator DestroyLight()
	{
		lightRadius.pointLightOuterRadius -= 0.2f;
		yield return new WaitForSeconds(0.05f);
		gameObject.tag = "Untagged";
		lightRadius.pointLightOuterRadius -= 0.2f;
		yield return new WaitForSeconds(0.05f);
		lightRadius.pointLightOuterRadius -= 0.2f;
		yield return new WaitForSeconds(0.05f);
		lightRadius.pointLightOuterRadius -= 0.2f;
		Destroy(gameObject);
	}
}
