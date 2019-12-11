using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool Active = false;
    private float timeActive = 5f;
    private float timeActivated;
    // Start is called before the first frame update
    void Start()
    {
        timeActivated = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
            Timer();
    }

    private void Timer()
    {
        if(Time.time - timeActivated > timeActive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            var bullet = collision.GetComponent<EnemyBullet>();
            Destroy(bullet.gameObject);
        }
    }
}
