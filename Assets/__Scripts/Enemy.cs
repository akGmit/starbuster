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


    // add sounds
    [SerializeField] private AudioClip spawnClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip crashClip;

    //private SoundController soundController;

    public int ScoreValue => scoreValue;
    
    public delegate void EnemyKilled(Enemy enemy);

    // game controller subscribes to this
    public static EnemyKilled EnemyKilledEvent;



    // == methods ==
    private void Start()
    {
        //soundController = SoundController.FindSoundController();
        //PlayClip(spawnClip);
    }

    // handle the collisions
    private void OnTriggerEnter2D(Collider2D WhatHitMe)
    {
        // have an object that hit me
        // if it's a bulett - then I have to die (and destroy the bullet)
        var bullet = WhatHitMe.GetComponent<PlayerBullet>();
        var player = WhatHitMe.GetComponent<Player>();
        // get the tag off the current gameobject
        string tagType = gameObject.tag;

        if (bullet && tagType == "EnemyEasy")
        {
            // it's time to die
            // get rid of the bullet object, and this object
            //PlayClip(hitClip);
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
        if (EnemyKilledEvent != null)
        {
            // there is a listener, so publish
            EnemyKilledEvent(this);
        }
    }

    private void PlayClip(AudioClip clip)
    {
        //if (soundController)
        //{
        //    soundController.PlayClipOnce(clip);
        //}
    }
}
