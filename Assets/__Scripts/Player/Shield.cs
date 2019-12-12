using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Shield powerup script.
/// Set time to live for shield.
/// Describe collision with enemy or bullet actions.
/// </summary>
public class Shield : MonoBehaviour
{
    private float timeActive = 5f;
    private Player player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Destroy(gameObject, timeActive);
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        var bullet = collision.GetComponent<EnemyBullet>();
        if (collision && bullet)
        {
            Destroy(bullet.gameObject);
        }else if(collision && enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
