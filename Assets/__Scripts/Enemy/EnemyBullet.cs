using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy bullet script.
/// Bullet is rotated towards the direction it is shot at.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float timeToLive = 5f;

    internal Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, timeToLive);
    }

    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}
