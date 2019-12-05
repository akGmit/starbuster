using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField]
    private string level;

    private int strength = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D WhatHitMe)
    {
        
        var bullet = WhatHitMe.GetComponent<PlayerBullet>();
        var player = WhatHitMe.GetComponent<Player>();
        // get the tag off the current gameobject
        string tagType = gameObject.tag;
        if (bullet && strength > 0)
        {
            Debug.Log("DDDDDD");
            Destroy(bullet.gameObject);
            strength--;
        }
        else if(strength <= 0)
        {
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(bullet.gameObject);
            PublishEnemyKilledEvent();

            Destroy(gameObject);
        }
    }
}
