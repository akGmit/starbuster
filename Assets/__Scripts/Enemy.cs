using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // == fields ==
    // use this later for scores and collisions
    [SerializeField]
    internal int scoreValue = 10;
    
    [SerializeField]
    internal ParticleSystem explosion;

    public int ScoreValue => scoreValue;

    public delegate void EnemyKilled(Enemy enemy);
    public static EnemyKilled EnemyKilledEvent;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D WhatHitMe)
    {
        var bullet = WhatHitMe.GetComponent<PlayerBullet>();
        var player = WhatHitMe.GetComponent<Player>();

        string tagType = gameObject.tag;

        if (bullet && tagType == "EnemyEasy")
        {
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(bullet.gameObject);
            PublishEnemyKilledEvent();
            Destroy(gameObject);
        }
        else if (bullet && tagType == "EnemySniper")
        {
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(bullet.gameObject);
            PublishEnemyKilledEvent();
            Destroy(gameObject);
        }
    }

    internal void PublishEnemyKilledEvent()
    {
        EnemyKilledEvent?.Invoke(this);
    }

    private void PlayClip(AudioClip clip)
    {
        //if (soundController)
        //{
        //    soundController.PlayClipOnce(clip);
        //}
    }
}
