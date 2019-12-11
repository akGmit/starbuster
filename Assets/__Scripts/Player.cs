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
    
    [SerializeField]
    private Shield shieldPrefab;

    //[SerializeField]
    //private Beam beamPrefab;

    private Shield shield;
    public int Score{ get; set; }
    public string Name { get; set; } = "default";

    private bool shieldActive = false;

    void Start()  
    {
        
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(shield)
        {
            shield.transform.position = transform.position;
        }
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * playerSpeed;
        var newXPos = transform.position.x + deltaX;
        transform.position = new Vector2(newXPos, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var tag = collision.tag;
        if (collision && tag == "EnemyBullet")
        {
            var collisionObj  = collision.GetComponent<EnemyBullet>();
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(collisionObj.gameObject);
            PublishPlayerKilledEvent();
            Destroy(gameObject);
        }
    }

    public void ActivateShield()
    {
        shield = Instantiate(shieldPrefab);
    }

    public delegate void PlayerKilled();     
    public static PlayerKilled PlayerKilledEvent;

    private void PublishPlayerKilledEvent() => PlayerKilledEvent?.Invoke();
}
