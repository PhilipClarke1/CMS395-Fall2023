using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;    // Fighter speed
    public Rigidbody2D fighter;
    public Animator animator;
    public float jumpForce = 1f;    // Jump force

    private bool isGrounded;          // To know if the fighter is on the ground

    void Start()
    {
        fighter = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        AnimateMovement();
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
{
    float move = 0f;

    if (Input.GetKey(KeyCode.RightArrow))
    {
        move = moveSpeed;
        animator.SetFloat("speed", 1); // Set speed parameter immediately
    }
    else if (Input.GetKey(KeyCode.LeftArrow))
    {
        move = -moveSpeed;
        animator.SetFloat("speed", 1); // Set speed parameter immediately
    }
    else
    {
        animator.SetFloat("speed", 0);
    }

    fighter.velocity = new Vector2(move, fighter.velocity.y);
}


    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            fighter.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            isGrounded = false;  // Reset grounded status after jumping
        }
    }

    void AnimateMovement()
    {
        if (Mathf.Abs(fighter.velocity.x) > 0.01f)
            animator.SetFloat("speed", Mathf.Abs(fighter.velocity.x));
        else
            animator.SetFloat("speed", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("ground"))
    {
        isGrounded = true;
    }
}

private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("ground"))
    {
        isGrounded = false;
    }
}
}


