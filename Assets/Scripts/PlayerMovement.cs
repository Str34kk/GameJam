using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Controller2D controller;
    public Animator animator;
    public CinemachineVirtualCamera vCam;
    public Transform cameraCenter;
    public Transform playerCenter;
    public GameObject throwablObject;
    public GameObject playerLight;
    public Light2D playerLightRadius;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private bool cameraOnPlayer = true;
    private bool jump = false;

    //bool dash = false;

    [System.Obsolete]
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("JumpUp", true);
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("JumpUp", true);
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("IsJumping", true);
            animator.SetBool("JumpUp", true);
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(!cameraOnPlayer)
            {
                vCam.m_Follow = playerCenter;
                vCam.m_Lens.OrthographicSize = 4;
                cameraOnPlayer = !cameraOnPlayer;
            }
            else if (cameraOnPlayer)
            {
                vCam.m_Follow = cameraCenter;
                vCam.m_Lens.OrthographicSize = 5;
                cameraOnPlayer = !cameraOnPlayer;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && (playerLightRadius.pointLightOuterRadius > 0.3f))
        {
            playerLightRadius.pointLightOuterRadius -= 0.2f;
            GameObject throwableLight = Instantiate(throwablObject, transform.position + new Vector3(transform.localScale.x * 0.5f, -0.2f), Quaternion.identity) as GameObject;
            Vector2 direction = new Vector2(transform.localScale.x, 0);
            throwableLight.GetComponent<Throwable>().direction = direction;
            throwableLight.name = "ThrowableLight";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
        //dash = false;
    }

    public void OnFall()
    {
        animator.SetBool("IsJumping", true);
    }

    public void OnLanding()
    {
        animator.SetBool("IsDoubleJumping", false);
        animator.SetBool("IsJumping", false);
    }

    [System.Obsolete]
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ThrowableLight")
        {
            playerLightRadius.pointLightOuterRadius += 0.2f;
        }

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Spikes")
        {
            StartCoroutine(controller.WaitToDead());
        }
    }
}
