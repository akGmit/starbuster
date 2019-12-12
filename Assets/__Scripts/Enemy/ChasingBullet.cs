using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBullet : EnemyBullet
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
        Destroy(gameObject, 3.5f);
    }

    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        if (player)
        {
            rb.velocity = player.transform.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
