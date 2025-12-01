using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movSpeed;

    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        Vector2 currentPosition = transform.position;

        float directionX = player.position.x - transform.position.x;
        float directionY = player.position.y - transform.position.y;
        Vector2 direction = new Vector2(directionX, directionY).normalized;

        float distanceToPlayer = (float)Math.Abs(Math.Sqrt(Math.Pow(directionX, 2) + Math.Pow(directionY, 2)));

        if (distanceToPlayer > 1.5f)
        {
            transform.position = currentPosition + (direction * movSpeed * Time.deltaTime);
        }

    }
}
