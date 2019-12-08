﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField]
    private PlayerBullet bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    private float firingRate = 0.2f;

    void Start()
    {
        InvokeRepeating("Shoot", 0f, firingRate);
    }

    void Update()
    { 
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        var b = transform.position.y + 0.5f;
        bullet.transform.position = new Vector2(transform.position.x, b);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.velocity = Vector2.up * bulletSpeed;
    }
}
