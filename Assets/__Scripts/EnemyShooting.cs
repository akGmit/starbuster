using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private EnemyBullet bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    private float firingRate = 0.2f;
    // Start is called before the first frame update

    private GameObject player;

    void Start()
    {      
        player = GameObject.Find("Player");
        var tag = gameObject.tag;

        if(tag == "EnemyEasy")
        {
            InvokeRepeating("ShootStraightDown", 0f, firingRate);
        }
        else if(tag == "EnemySniper")
        {
            InvokeRepeating("ShootAtPlayer", 0f, firingRate);
        }
        else if (tag == "Boss")
        {
            InvokeRepeating("BossShooting", 0f, firingRate);
        }
    }

    private void BossShooting()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = TargetDirection() * bulletSpeed;
    }

    private void ShootAtPlayer()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = TargetDirection() * bulletSpeed;
    }

    private void ShootStraightDown()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
    }

    private Vector3 TargetDirection()
    {
        return (player.transform.position - transform.position).normalized;
    }
}
