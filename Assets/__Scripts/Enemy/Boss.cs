using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script representing Boss enemy game object. Boss extends Enemy class.
/// Boss is Enemy but has other unique properties. Unlike Enemy objects movement,
/// Boss movement is random.
/// </summary>
public class Boss : Enemy
{
    #region Private memebers
    private Rigidbody2D rb;
    private float latestDirectionChangeTime = 0f;
    private readonly float directionChangeTime = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    #endregion

    //Initialiaze variables; Start object movement
    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        strength = settings.EnemyStrength * 7;
        SetMovementVector();     
    }

    void Update()
    {
        /* Check if last movement direction change exceeded limit.
         * IF true reset direction change time, change direction
         */
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            SetMovementVector();
        }
    }

    /* Set movement direction and speed of this object.
     * Movement directions is created using random  values.
     */
    private void SetMovementVector()
    {
        movementDirection = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * settings.EnemySpeed * 1.2f;
        rb.velocity = movementPerSecond;
    }

    /// <summary>
    /// Collision definitions.
    /// Two types of collisions: with enemy body and enemy bullet with different outcomes.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If triggerd of Edges, reverse body velocity
        if (collision.CompareTag("Edges"))
        {
            rb.velocity *= -0.5f;
        }

        var bullet = collision.GetComponent<PlayerBullet>();
        var beam = collision.GetComponent<Beam>();
        
        // Collisions with player bullets and beam
        if (collision && bullet)
        {
            if (bullet.CompareTag("PowerUpBeam"))
            {
                strength -= settings.LevelNumber * 2;
                if (strength <= 0)
                {
                    explosion.transform.position = transform.position;
                    Instantiate(explosion);
                    PublishEnemyKilledEvent();
                    Destroy(gameObject);
                }
            }else
            {
                CollisionOutcome( 1, bullet.gameObject);
            }
        }
        
    }

    // Helper method to determine collision outcome
    private void CollisionOutcome(int damage, GameObject other)
    {
        strength -= damage;
        if (strength > 0)
        {
            Destroy(other);
        }
        else
        {
            FatalCollision(other);
        }
    }

    // Method to execute code for collision which resulted in players death.
    private void FatalCollision(GameObject other)
    {
        explosion.transform.position = transform.position;
        Instantiate(explosion);
        Destroy(other);
        PublishEnemyKilledEvent();
        Destroy(gameObject);
    }
}
