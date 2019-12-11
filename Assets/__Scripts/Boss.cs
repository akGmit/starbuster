using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private ParticleSystem bossAppearFX;

    private LevelSettings settings;
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
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        var collider = gameObject.GetComponent<PolygonCollider2D>();
        strength = settings.EnemyStrength * 10;
        SetMovementVector();     
    }

    private void SetMovementVector()
    {
        movementDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * settings.EnemySpeed * 7;
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
        if (WhatHitMe.CompareTag("PlayerBullet"))
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
