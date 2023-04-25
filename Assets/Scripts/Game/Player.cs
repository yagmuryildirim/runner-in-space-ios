using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public bool extraLife;

    [Header("Speed")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float milestoneIncrease;
    private float speedMilestone;
    private float defaultSpeed;
    private float defaultMilestoneIncrease;

    [Header("Movement")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    [Header("Collision")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float ceilingCheckDistance;
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;

    public bool isDead;
    private bool canDoubleJump;
    private bool isGrounded;
    private bool wallDetected;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isRunning = false;
        speedMilestone = milestoneIncrease;
        defaultSpeed = moveSpeed;
        defaultMilestoneIncrease = milestoneIncrease;
    }

    private void Update()
    {
        AnimatorControllers();
        CheckCollision();
        extraLife = moveSpeed >= maxSpeed;

        if (isRunning) Movement();

        if (isGrounded) canDoubleJump = true;

        CheckInput();
        SpeedController();
    }

    public void SetBoost(BoostType boostType)
    {
        if (boostType == BoostType.speed) moveSpeed *= 1.5f;
        else if (boostType == BoostType.jump) jumpForce *= 1.4f;
    }

    private void Movement()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }


    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager.instance.PlaySFX(0);
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            AudioManager.instance.PlaySFX(1);
            canDoubleJump = false;
        }
    }

    private void SpeedReset()
    {
        moveSpeed = defaultSpeed;
        milestoneIncrease = defaultMilestoneIncrease;
    }

    private void SpeedController()
    {
        if (moveSpeed == maxSpeed) return;
        if (transform.position.x > speedMilestone)
        {
            speedMilestone += milestoneIncrease;
            moveSpeed *= speedMultiplier;
            milestoneIncrease *= speedMultiplier;

            if (moveSpeed > maxSpeed) moveSpeed = maxSpeed;
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }


    private void CheckCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);
    }

    private void AnimatorControllers()
    {
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetFloat("xVelocity", rb.velocity.x);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("canDoubleJump", canDoubleJump);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + ceilingCheckDistance));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }
}
