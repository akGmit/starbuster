using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float timeActive;
    private float timeActivated;
    private LevelSettings settings;
    private int hitCount = 3;
    
    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        timeActive = 7f;
        timeActivated = Time.time;
    }

    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (Time.time - timeActivated > timeActive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
  
        if(collision && enemy)
        {
            hitCount--;
            if(hitCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
