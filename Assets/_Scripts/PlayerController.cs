using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

/// <summary>
/// File Name: PlayerController.cs
/// Author: Kevin Yuayan
/// Last Modified by: Kevin Yuayan
/// Date Last Modified: Nov. 3, 2019
/// Description: Controller for the Player Object
/// Revision History:
/// </summary>
public class PlayerController : MonoBehaviour
{

    public PlayerAnimState playerAnimState;
    public PlayerJumpState playerJumpState;

    [Header("GameObjects")]
    public GameObject gameControllerObject;

    [Header("Object Properties")]
    public Animator playerAnimator;
    public SpriteRenderer playerSpriteRenderer;
    public Rigidbody2D playerRigidBody;

    [Header("Physics Related")]
    public float moveForce;
    public float jumpForce;
    public bool isGrounded;
    //public Transform groundTarget;
    public Vector2 maximumVelocity = new Vector2(20.0f, 30.0f);

    [Header("Sounds")]
    public AudioSource jumpSound;
    public AudioSource pickupSound;

    private float jumpCooldownTime = 0.0f;
    //player can only jump every 0.5s
    private float jumpCooldown = 0.5f;

    private GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        playerAnimState = PlayerAnimState.IDLE;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckJumpState();
    }

    // Checks RigidBody2D to determine jump state/animation
    private void CheckJumpState()
    {
        if (playerRigidBody.velocity.y == 0)
        {
            playerJumpState = PlayerJumpState.LAND;
        }
        if (playerRigidBody.velocity.y > 0.1f)
        {
            playerJumpState = PlayerJumpState.UP;
        }
        if (playerRigidBody.velocity.y < -0.1f)
        {
            playerJumpState = PlayerJumpState.DOWN;
        }

        playerAnimator.SetInteger("JumpState", (int)playerJumpState);
    }

    // Handles Player movement
    private void Move()
    {

        isGrounded = Physics2D.BoxCast(
            transform.position - new Vector3(0, playerSpriteRenderer.bounds.extents.y + 0.08f, 0), new Vector2(0.9f, 0.08f), 0.0f, Vector2.down, 0.08f, 1 << LayerMask.NameToLayer("Ground"));
    
        // Idle State
        if (Input.GetAxis("Horizontal") == 0)
        {
            playerAnimState = PlayerAnimState.IDLE;
        }
        
        // Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            playerRigidBody.AddForce(new Vector2(1, 0) * moveForce);

            if (isGrounded)
            {
                playerAnimState = PlayerAnimState.RUN;
            }
        }

        // Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            playerRigidBody.AddForce(new Vector2(-1, 0) * moveForce);

            if (isGrounded)
            {
                playerAnimState = PlayerAnimState.RUN;
            }
        }
        jumpCooldownTime += Time.deltaTime;
        // Jump
        if ((Input.GetAxis("Jump") > 0) && (isGrounded) && jumpCooldownTime >= jumpCooldown)
        {
            playerAnimState = PlayerAnimState.JUMP;
            jumpSound.Play();
            playerRigidBody.AddForce(Vector2.up * jumpForce);
            isGrounded = false;
            jumpCooldownTime = 0.0f;
            //jumpSound.Play();
        }

        playerAnimator.SetInteger("AnimState", (int)playerAnimState);

        playerRigidBody.velocity = new Vector2(
            Mathf.Clamp(playerRigidBody.velocity.x, -maximumVelocity.x, maximumVelocity.x),
            Mathf.Clamp(playerRigidBody.velocity.y, -maximumVelocity.y, maximumVelocity.y)
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            gameController.Lives -= 1;
        }
        if (other.gameObject.CompareTag("Fruit"))
        {
            pickupSound.Play();
            other.gameObject.SetActive(false);
            gameController.Score += 100;
        }
    }
}
