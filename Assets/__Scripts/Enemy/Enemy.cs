using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script which represents Enemy type game object.
/// Definded here are collisions handler and EnemyKilled event.
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    internal int scoreValue = 10;
    
    [SerializeField]
    internal ParticleSystem explosion;
    #endregion

    public int ScoreValue => scoreValue;
    internal LevelSettings settings;
    internal int strength;

    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        scoreValue = settings.EnemyScoreValue;
        strength = settings.EnemyStrength;
    }

    /// <summary>
    /// Collision action definitions.
    /// Collisions with player bullet, player itself and power up beam are dealt with.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<PlayerBullet>();
        var player = collision.GetComponent<Player>();
        var beam = collision.GetComponent<Beam>();

        
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
            }
            else
            {
                CollisionOutcome( 1, bullet.gameObject);
            }
        }
        else if (collision && player)
        {
            Destroy(gameObject);
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
        PublishEnemyKilledEvent();
        Destroy(gameObject);
    }

    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent;

    internal void PublishEnemyKilledEvent()
    {
        EnemyKilledEvent?.Invoke(this);
    }

    private void PlayClip(AudioClip clip)
    {
        //if (soundController)
        //{
        //    soundController.PlayClipOnce(clip);
        //}
    }
}
