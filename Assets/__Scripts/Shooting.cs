using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // player fires on space bar
    // == fields ==
    [SerializeField]
    private PlayerBullet bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    private float firingRate = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 0f, firingRate);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        var b = transform.position.y + 0.5f;
        bullet.transform.position = new Vector2(transform.position.x, b);
        //bullet.transform.position.y = transform.position.y + 3;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.velocity = Vector2.up * bulletSpeed;
        // play shoot sound
        //PlayClip(shootClip);
    }
}
