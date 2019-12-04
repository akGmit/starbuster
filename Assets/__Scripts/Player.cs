using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // == fields ==
    [SerializeField]    // adds this field to the Unity editor
    private float playerSpeed = 5.0f;

    [SerializeField]
    private ParticleSystem explosion;
    
    public int Score { get; set; }
    // Start is called before the first frame update
    void Start()    // initialisation
    {
    }

    // == private methods ==
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // need to ensure that deltaX is frame rate independent
        // use Time.deltaTime as a multiplier
        // also add a speed multiplier for the player to get a good feel
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        
        // add the delta to the current position
        var newXPos = transform.position.x + deltaX;
        
        // update the current position
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<EnemyBullet>();
        var enemy = collision.GetComponent<Enemy>();

        if (bullet)
        {
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(bullet.gameObject);
            PublishPlayerKilledEvent();
            Destroy(gameObject);
        }
    }

    //An delegate type for player death event
    //Main controller willl deal with this
    public delegate void PlayerKilled(Player player);
        
    public static PlayerKilled PlayerKilledEvent;

    private void PublishPlayerKilledEvent()
    {
        if (PlayerKilledEvent != null)
        {
            // there is a listener, so publish
            PlayerKilledEvent(this);
        }
    }
}
