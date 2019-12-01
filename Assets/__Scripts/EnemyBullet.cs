using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 3.0f;

    private void Start()
    {
        Destroy(this.gameObject, timeToLive);
    }
}
