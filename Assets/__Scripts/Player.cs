using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private float playerSpeed = 5.0f;

    [SerializeField]
    private ParticleSystem explosion;
    
    public int Score { get; set; }

    void Start()  
    {
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = transform.position.x + deltaX;
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bullet = collision.GetComponent<EnemyBullet>();
        var enemy = collision.GetComponent<Enemy>();

        if (bullet || enemy)
        {
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(bullet.gameObject);
            PublishPlayerKilledEvent();
            Destroy(gameObject);
        }
    }


    public delegate void PlayerKilled(Player player);     
    public static PlayerKilled PlayerKilledEvent;

    private void PublishPlayerKilledEvent() => PlayerKilledEvent?.Invoke(this);
}
