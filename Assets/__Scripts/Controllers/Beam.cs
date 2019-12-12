using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    private float timeActive;
    private float timeActivated;
    private int hitCount = 3;
    
    void Start()
    {
        Destroy(gameObject, 4f);
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
