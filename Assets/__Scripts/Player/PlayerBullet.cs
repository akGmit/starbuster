using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 3.0f;

    private void Start()
    {
        Destroy(this.gameObject, timeToLive);
    }
}

