using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour
{
    public Transform fighter;
    public float moveSpeed = 1.0f;
    public float moveDuration = 1.0f;
    public float stopDuration = 1.0f;
    public Animator animator;


    private bool canMove = true;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask fighterLayers;  

    private bool isMoving = true;

    void Start()
    {
        GameObject fighterObject = GameObject.Find("fighter");
        if (fighterObject != null)
        {
            fighter = fighterObject.transform;
        }
        else
        {
            Debug.LogError("Fighter object not found");
        }

        StartCoroutine(MoveAndStopRoutine());
        StartCoroutine(PunchRoutine());
    }

    void Update()
    {
        if (fighter != null)
        {
            if (isMoving)
            {
                MoveTowardsFighter();
                FlipSprite();
            }
            animator.SetBool("IsWalking", isMoving);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Fighter"))
    {
        canMove = false;
        StartCoroutine(ResumeMovementAfterDelay(2.0f)); // Resume movement after 2 seconds
    }
}

    IEnumerator ResumeMovementAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    canMove = true;
}


    IEnumerator MoveAndStopRoutine()
    {
        while (true)
        {
            isMoving = true;
            yield return new WaitForSeconds(moveDuration);

            isMoving = false;
            yield return new WaitForSeconds(stopDuration);
        }
    }

    IEnumerator PunchRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // Wait for 2 seconds

            if (Random.value > 0f) // 50% chance to punch
            {
                punch();
            }
        }
    }

    void MoveTowardsFighter()
{
    if (isMoving && canMove) // Check if the enemy can move
    {
        // Calculate direction but only use the horizontal component (x-axis)
        Vector3 direction = (fighter.position - transform.position).normalized;
        direction.y = 0; // This ensures the enemy only moves horizontally

        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}


    void FlipSprite()
    {
        if (fighter != null)
        {
            bool shouldFaceRight = fighter.position.x > transform.position.x;
            if ((shouldFaceRight && transform.localScale.x < 0) || (!shouldFaceRight && transform.localScale.x > 0))
            {
                
            }
            else{
                if (isMoving)
                {
                    Flip();
                }
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void punch(){
        
        animator.SetTrigger("Punch");

        Collider2D[] hitFighters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, fighterLayers);

        // damage fighter 
        foreach(Collider2D fighter in hitFighters){

            fighter.GetComponent<FighterMovement>().TakeDamage(25);

        }
    }

     void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }   
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
