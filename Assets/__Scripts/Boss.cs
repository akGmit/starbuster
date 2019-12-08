using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private string level;
    private int strength;
    private Rigidbody2D rb;
    private float latestDirectionChangeTime = 0f;
    private readonly float directionChangeTime = 2f;
    private float characterVelocity = 1.5f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        strength = Levels.Level[level].EnemyStrength * 10;
        SetMovementVector();
    }

    private void SetMovementVector()
    {
        movementDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * Levels.Level[level].EnemySpeed * 7;
        rb.velocity = movementPerSecond;
    }

    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            SetMovementVector();
        }
    }

    private void OnTriggerEnter2D(Collider2D WhatHitMe)
    {
        if (WhatHitMe.CompareTag("Edges"))
        {
            rb.velocity *= -1;
            latestDirectionChangeTime = Time.time;
        }
        if (WhatHitMe.CompareTag("Player"))
        {
            var bullet = WhatHitMe.GetComponent<PlayerBullet>();
     
            if (bullet && strength > 0)
            {
                Destroy(bullet.gameObject);
                strength--;
            }
            else if (bullet && strength <= 0)
            {
                explosion.transform.position = transform.position;
                Instantiate(explosion);
                Destroy(bullet.gameObject);
                PublishEnemyKilledEvent();
                Destroy(gameObject);
            }
        }
    }
}
