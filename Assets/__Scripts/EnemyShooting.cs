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
    void Start()
    {
        var tag = gameObject.tag;

        if(tag == "EnemyEasy")
        {
            InvokeRepeating("ShootStraightDown", 0f, firingRate);
        }else if(tag == "EnemySniper")
        {
            
            InvokeRepeating("ShootAtPlayer", 0f, firingRate);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShootAtPlayer()
    {
        var playerPosition = GameObject.Find("Player").transform.position;
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
     
        rb.velocity = Vector2.MoveTowards(rb.position, playerPosition, bulletSpeed);
        
    }

    private void ShootStraightDown()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
        // play shoot sound
        //PlayClip(shootClip);
    }
}
