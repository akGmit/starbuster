using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PublishPowerUpCollectedEvent();
            Destroy(gameObject);
        }
    }

    public delegate void PowerUpCollected(string p);
    public static PowerUpCollected PowerUpCollectedEvent;

    private void PublishPowerUpCollectedEvent() => PowerUpCollectedEvent?.Invoke(gameObject.tag);
}
