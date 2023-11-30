using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;    // speed of fighter
    public Rigidbody2D fighter;
    public Animator animator;
    public float jumpForce = 1f;   

    public Transform attackpoint; 
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackOneDamage = 25;
    public int attackTwoDamage = 30;
    public int attackThreeDamage = 40;

    private bool isGrounded;          // To know if the fighter is on the ground

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Start()
    {
        fighter = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   

        if(Time.time >= nextAttackTime){

            if(Input.GetKeyDown(KeyCode.K)){
                HandleAttack();
                nextAttackTime = Time.time + 1f /attackRate;
            }

        }
        AnimateMovement();  
        HandleMovement();
        HandleJump();
        HandleAttack();
        HandleAttack2();
        HandleAttack3();

    }

//     void OnDrawGizmos() {
//     Gizmos.color = Color.red;
//     Gizmos.DrawWireSphere(transform.position, 0.5f); // Draws a red sphere with a radius of 0.5 units
// }

    void HandleAttack(){

        if(Input.GetKeyDown(KeyCode.K)){
            PerformAttack();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            // apply damage
            foreach(Collider2D enemy in hitEnemies) {


            enemy.GetComponent<enemyHealth>().TakeDamage(attackOneDamage);

        }
        }
        

        
    }

    void OnDrawGizmosSelected(){
        if(attackpoint == null){
            return;
        }   
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }

    void HandleAttack2(){

        if(Input.GetKeyDown(KeyCode.L)){
            PerformAttack2();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            // apply damage
            foreach(Collider2D enemy in hitEnemies) {


            enemy.GetComponent<enemyHealth>().TakeDamage(attackTwoDamage);
        }
    }

    }
    void HandleAttack3(){

        if(Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K)){
            PerformAttack3();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            // apply damage
            foreach(Collider2D enemy in hitEnemies) {


            enemy.GetComponent<enemyHealth>().TakeDamage(attackThreeDamage);
        }
    }
    }


    void HandleMovement()
{
    float move = 0f;
    
    if (Input.GetKey(KeyCode.D))
    {
        move = moveSpeed;
        animator.SetFloat("speed", 1);
        transform.localScale = new Vector3(1.5f, 1.5f, 1); // Facing right
    }
    else if (Input.GetKey(KeyCode.A))
    {
        move = -moveSpeed;
        animator.SetFloat("speed", 1);
        transform.localScale = new Vector3(-1.5f, 1.5f, 1); // Facing left
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


