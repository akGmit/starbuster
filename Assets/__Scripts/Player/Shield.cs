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
    public bool Active = false;
    private float timeActive = 5f;

    void Start()
    {
        Destroy(gameObject, timeActive);
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
