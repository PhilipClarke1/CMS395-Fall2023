using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{


    public float moveSpeed = 5.0f;    // speed of fighter
    public Rigidbody2D fighter;
    public Animator animator;
    public float jumpForce = 10f;

    public soundeffectplayer soundEffectPlayer;

    public int totalDamageDealt = 0;

    public Transform healthBarPosition;


    public healthBar healthBar;

    public GameOverScreen GameOverScreen;

    public int maxHealth = 100;
    int currentHealth;

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
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        soundEffectPlayer = GetComponent<soundeffectplayer>();

    }

    void Update()
    {

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                HandleAttack();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                HandleAttack2();
            }
            if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K))
            {
                HandleAttack3();
            }
        }
        AnimateMovement();  
        HandleMovement();
        HandleJump();
        //HandleAttack();
        //HandleAttack2();
        //HandleAttack3();

    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0 ){
            Die();
        }
    }

    void Die()
{
    animator.SetBool("IsDead", true);
    StartCoroutine(ShowGameOverScreenAfterDelay(2.5f));

        DisableCombatComponents();
    }

void DisableCombatComponents()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        this.enabled = false;

        // Optionally, disable Rigidbody2D if you don't want the character to be affected by physics anymore
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

    }
IEnumerator ShowGameOverScreenAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    GameOverScreen.Setup(totalDamageDealt);
}


//     void OnDrawGizmos() {
//     Gizmos.color = Color.red;
//     Gizmos.DrawWireSphere(transform.position, 0.5f); // Draws a red sphere with a radius of 0.5 units
// }

    void HandleAttack(){

        if (Time.time >= nextAttackTime)
        {
            PerformAttack();
            ApplyDamage(attackOneDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (Input.GetKeyDown(KeyCode.K)){
            PerformAttack();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            // apply damage
            foreach(Collider2D enemy in hitEnemies) {


            enemy.GetComponent<enemyHealth>().TakeDamage(attackOneDamage);
            totalDamageDealt += attackOneDamage;
                soundEffectPlayer.swordNoHit();

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

        if (Time.time >= nextAttackTime)
        {
            PerformAttack2();
            ApplyDamage(attackTwoDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (Input.GetKeyDown(KeyCode.L)){
            PerformAttack2();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            // apply damage
            foreach(Collider2D enemy in hitEnemies) {


            enemy.GetComponent<enemyHealth>().TakeDamage(attackTwoDamage);
            totalDamageDealt += attackTwoDamage;
        }
            soundEffectPlayer.swordNoHit();
        }

    }
    void HandleAttack3(){

        if (Time.time >= nextAttackTime)
        {
            PerformAttack3();
            ApplyDamage(attackThreeDamage);
            nextAttackTime = Time.time + 1f / attackRate;
        }

        if (Input.GetKeyDown(KeyCode.L) && Input.GetKeyDown(KeyCode.K)){
            PerformAttack3();
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);

            // apply damage
            foreach(Collider2D enemy in hitEnemies) {


            enemy.GetComponent<enemyHealth>().TakeDamage(attackThreeDamage);
            totalDamageDealt += attackThreeDamage;
        }
            soundEffectPlayer.swordNoHit();
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

    void ApplyDamage(int damageAmount)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemyHealth>().TakeDamage(damageAmount);
            totalDamageDealt += damageAmount;
            soundEffectPlayer.swordNoHit();
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


