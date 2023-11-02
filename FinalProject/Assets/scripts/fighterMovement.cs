using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;    // speed of fighter
    public Rigidbody2D fighter;
    public Animator animator;
    public float jumpForce = 1f;    

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
        HandleAttack();
        HandleAttack2();
        HandleAttack3();

    }

    void HandleAttack(){

        if(Input.GetKeyDown(KeyCode.K)){
            PerformAttack();
        }
    }

    void HandleAttack2(){

        if(Input.GetKeyDown(KeyCode.L)){
            PerformAttack2();
        }
    }

    void HandleAttack3(){

        if(Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K)){
            PerformAttack3();
        }
    }


    void HandleMovement()
{
    float move = 0f;

    if (Input.GetKey(KeyCode.D))
    {
        move = moveSpeed;
        animator.SetFloat("speed", 1); 
    }
    else if (Input.GetKey(KeyCode.A))
    {
        move = -moveSpeed;
        animator.SetFloat("speed", 1); 
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

            // Debug.Log("Jump triggered in script");

            animator.SetBool("isJumping", true);

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

        animator.SetBool("isJumping", false);

    }
}
void PerformAttack(){
    animator.SetTrigger("attack");

    // Debug.Log("Attack performed");

}

void PerformAttack2(){

    animator.SetTrigger("attack2");
}

void PerformAttack3(){

    animator.SetTrigger("attack3");
}

private void OnCollisionExit2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("ground"))
    {
        isGrounded = false;
    }
}
}


