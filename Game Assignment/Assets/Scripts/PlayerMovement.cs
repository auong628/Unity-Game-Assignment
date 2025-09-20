using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private bool isAttacking = false;
    private bool isDead = false;

    // Optional ground check system (recommended)
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim = GetComponent<Animator>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isDead) return; // Prevent all actions if dead

        // Check if grounded (using overlap circle)
        if (groundCheck != null)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // If attacking, stop movement
        if (isAttacking)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            return;
        }

        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * moveSpeed, body.velocity.y);

        // Flip player left/right
        float spriteSize = 1.5f;
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(spriteSize, spriteSize, spriteSize);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-spriteSize, spriteSize, spriteSize);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
            Jump();

        // Update animator
        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        grounded = false;
    }

    // Called from attack script when starting/stopping attack
    public void SetAttacking(bool state)
    {
        isAttacking = state;
    }

    // Called from health script when dying
    public void Die()
    {
        isDead = true;
        body.velocity = Vector2.zero;
        anim.SetTrigger("die");
    }

    // Debug visualization for ground check
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
