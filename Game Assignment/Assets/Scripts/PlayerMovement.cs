using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;


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
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Adjusted for sprite size of 1.5
        float spriteSize = 1.5f;

        // Flip player when facing left/right.
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(spriteSize,
            spriteSize, spriteSize);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-spriteSize,
            spriteSize, spriteSize);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
