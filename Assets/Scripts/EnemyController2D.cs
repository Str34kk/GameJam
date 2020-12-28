using UnityEngine;
using UnityEngine.Events;

public class EnemyController2D : MonoBehaviour
{
	[SerializeField] private float JumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;
	[SerializeField] private LayerMask WhatIsGround;
	[SerializeField] private Transform GroundCheck;

	const float GroundedRadius = .2f;
	private bool Grounded;
	private Rigidbody2D enemyRigidbody2D;
	private Vector3 Velocity = Vector3.zero;
	private bool FacingRight = true;

	private void Start()
	{
		enemyRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
			}
		}
	}

	public void Move(float move, bool jump)
	{
		if (Grounded)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, enemyRigidbody2D.velocity.y);
			enemyRigidbody2D.velocity = Vector3.SmoothDamp(enemyRigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

			if (move > 0 && !FacingRight)
			{
				Flip();
			}
			else if (move < 0 && FacingRight)
			{
				Flip();
			}
		}

		if (Grounded && jump)
		{
			Grounded = false;
			enemyRigidbody2D.AddForce(new Vector2(0f, JumpForce));
		}
	}

	private void Flip()
	{
		FacingRight = !FacingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}