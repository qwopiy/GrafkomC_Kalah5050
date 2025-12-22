using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100;

    public SpriteRenderer spriteRenderer;
    public EnemyMovement enemyMovement;
    public Rigidbody2D rb;
    public Animator animator;

    public bool isDying = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMovement.enabled = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMovement.enabled = true;
            StartCoroutine(DelayAfterCollide());
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        StartCoroutine(GetHit());
        if (health <= 0)
        {
            enemyMovement.enabled = false;
            GameStateManager.AddKill();
            animator.SetTrigger("Die");
            StartCoroutine(DieDelay());
        }
    }

    IEnumerator DelayAfterCollide()
    {
        enemyMovement.movSpeed = (float)(enemyMovement.movSpeed * 0.2);
        yield return new WaitForSeconds(0.5f);
        enemyMovement.movSpeed = (float)(enemyMovement.movSpeed * 10/2);
    }
    IEnumerator GetHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    IEnumerator DieDelay()
    {
        isDying = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
