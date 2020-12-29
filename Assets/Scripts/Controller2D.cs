using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Controller2D : MonoBehaviour
{
	[SerializeField] private float jumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
	[SerializeField] private bool airControl = true;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private Transform ceilingCheck;
	[SerializeField] private AudioSource audioSrc;
	[SerializeField] private Animator animator;

	const float groundedRadius = .2f;
	private bool grounded;
	const float ceilingRadius = .2f;
	private Rigidbody2D playerRigidbody2D;
	private bool facingRight = true;
	private Vector3 velocity = Vector3.zero;
	private bool canDoubleJump = true;
	private bool canMove = true;
	private int canLand = 0;
	private AudioClip attackSound;
	private AudioClip runSound;
	private AudioClip jumpUpSound;
	private AudioClip jumpLandSound;
	private AudioClip duckSound;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public UnityEvent OnFallEvent;

	private void Awake()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 60;
		playerRigidbody2D = GetComponent<Rigidbody2D>();
		attackSound = Resources.Load<AudioClip>("Sound/Attack");
		runSound = Resources.Load<AudioClip>("Sound/Run");
		jumpUpSound = Resources.Load<AudioClip>("Sound/JumpUp");
		jumpLandSound = Resources.Load<AudioClip>("Sound/JumpLand");
		duckSound = Resources.Load<AudioClip>("Sound/Duck");
		animator = GetComponent<Animator>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;

				if (!wasGrounded)
                {
					OnLandEvent.Invoke();
					canDoubleJump = true;
					canLand += 1;
					if (canLand == 2)
                    {
						audioSrc.PlayOneShot(jumpLandSound);
						canLand = 0;
					}
				}
			}
		}

		if (!grounded)
		{
			OnFallEvent.Invoke();
		}
	}

	public void Move(float move, bool jump)
	{
		if(canMove)
        {
			if (grounded || airControl)
			{
				Vector3 targetVelocity = new Vector2(move * 10f, playerRigidbody2D.velocity.y);
				playerRigidbody2D.velocity = Vector3.SmoothDamp(playerRigidbody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

				if (move > 0 && !facingRight)
				{
					Flip();
				}
				else if (move < 0 && facingRight)
				{
					Flip();
				}
			}

			if (grounded && jump)
			{
				animator.SetBool("IsJumping", true);
				animator.SetBool("JumpUp", true);
				grounded = false;
				playerRigidbody2D.AddForce(new Vector2(0f, jumpForce));
				canDoubleJump = true;
				audioSrc.PlayOneShot(jumpUpSound);
			}
			else if (!grounded && jump && canDoubleJump)
			{
				canDoubleJump = false;
				playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, 0);
				playerRigidbody2D.AddForce(new Vector2(0f, jumpForce / 1.2f));
				audioSrc.PlayOneShot(jumpUpSound);
				//animator.SetBool("IsDoubleJumping", true);
				//animator.Play("DoubleRun");
			}
		}
    }

	public void ThrowLight()
    {
		audioSrc.PlayOneShot(attackSound);
	}

	private void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public IEnumerator WaitToDead()
	{
		canMove = false;
		animator.SetBool("IsDead", true);
		yield return new WaitForSeconds(0.4f);
		playerRigidbody2D.velocity = new Vector2(0, playerRigidbody2D.velocity.y);
		yield return new WaitForSeconds(0.6f);
		SceneManager.LoadSceneAsync("MainMenu");
	}
	public IEnumerator WaitToDuck()
	{
		yield return new WaitForSeconds(0.1f);
		audioSrc.PlayOneShot(duckSound);
	}
}