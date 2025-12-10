using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movSpeed;
    SpriteRenderer sr;
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Vector2 currentPosition = transform.position;

        float directionX = player.position.x - transform.position.x;
        float directionY = player.position.y - transform.position.y;
        Vector2 direction = new Vector2(directionX, directionY).normalized;

        float distanceToPlayer = (float)Math.Abs(Math.Sqrt(Math.Pow(directionX, 2) + Math.Pow(directionY, 2)));

        if (distanceToPlayer > 0f)
        {
            transform.position = currentPosition + (movSpeed * Time.deltaTime * direction);
        }

        if (directionX < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
