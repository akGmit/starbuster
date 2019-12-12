using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class representing Player object.
/// Player movement and collider is defiened here.
/// </summary>
public class Player : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] 
    private float playerSpeed = 5.0f;

    [SerializeField]
    private ParticleSystem explosion;
    
    [SerializeField]
    private Shield shieldPrefab;
    #endregion

    private Shield shield;
    private LevelSettings settings;
    private int strength = 20;
    public int Score{ get; set; }
    public string Name { get; set; }

    void Start()  
    {
        //Load specific level settings
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Move player according to input.
    /// This implementation allows only horizontal movement.
    /// If shield is active, this method sets shield position to players.
    /// </summary>
    private void Move()
    {
        
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = transform.position.x + deltaX;
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    /// <summary>
    /// Collision definitions.
    /// Two types of collisions: with enemy body and enemy bullet with different outcomes.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        var bullet = collision.GetComponent<EnemyBullet>();
        var powerup = collision.GetComponent<PowerUp>();

        if (collision && bullet)
        {
            CollisionOutcome(settings.EnemyShotDamage, bullet.gameObject);
        }
        else if (collision && enemy)
        {
            CollisionOutcome(settings.EnemyCollisionDamage, enemy.gameObject);
        }
        else if (collision && powerup)
        {
            if (powerup.CompareTag("CollectableFiringRate"))
            {
                var shoot = gameObject.GetComponent<Shooting>();
                shoot.IncreaseFiringRate();
                Destroy(powerup);
            }
        }
    }

    //Helper method to determine collision outcome
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
        PublishPlayerKilledEvent();
        Destroy(gameObject);
    }

    /// <summary>
    /// Instantiate shield powerup.
    /// </summary>
    public void ActivateShield()
    {
        Instantiate(shieldPrefab);
    }

   
    //Player killed event
    public delegate void PlayerKilled();     
    public static PlayerKilled PlayerKilledEvent;
    private void PublishPlayerKilledEvent() => PlayerKilledEvent?.Invoke();
}
