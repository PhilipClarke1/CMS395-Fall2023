using System.Collections;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    public healthBar enemyhealthBar;
    public NextLevelScreen NextLevelScreen;


    void Start()
    {
        currentHealth = maxHealth;
        enemyhealthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyhealthBar.SetHealth(currentHealth);

        // Play damage animation  
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Debug.Log("The enemy is dead");

        // Show death animation 
        animator.SetBool("IsDead", true);

        // Start the fade-out coroutine
        StartCoroutine(FadeOut(3f)); // 1 second fade-out time
        StartCoroutine(ShowNextLevelScreenAfterDelay(2.5f));


        //NextLevelScreen.Setup();

    }


    IEnumerator ShowNextLevelScreenAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        NextLevelScreen.Setup();
    }
    IEnumerator FadeOut(float fadeTime)
    {
        float startAlpha = spriteRenderer.color.a;

        for (float t = 0; t < 1; t += Time.deltaTime / fadeTime)
        {
            Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(startAlpha, 0, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Disable or destroy the enemy after fade-out
        gameObject.SetActive(false); // or Destroy(gameObject);
    }
}
