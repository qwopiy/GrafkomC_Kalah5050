using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100;

    public SpriteRenderer spriteRenderer;
    public EnemyMovement enemyMovement;
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
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
            StartCoroutine(delayAfterCollide());
        }
    }

    public  void TakeDamage(float amount)
    {
        health -= amount;
        StartCoroutine(getHit());
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator delayAfterCollide()
    {
        enemyMovement.movSpeed = (float)(enemyMovement.movSpeed * 0.2);
        yield return new WaitForSeconds(0.5f);
        enemyMovement.movSpeed = (float)(enemyMovement.movSpeed * 10/2);
    }
    IEnumerator getHit()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
    }
}
