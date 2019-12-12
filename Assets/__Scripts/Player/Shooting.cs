using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Describe shooing behaviour of a player.
/// Player shooting is set to fixed interval and is controlled by script.
/// </summary>
public class Shooting : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    private PlayerBullet bulletPrefab;

    [SerializeField]
    private float bulletSpeed = 5f;

    [SerializeField]
    private float firingRate = 0.2f;

    [SerializeField]
    private PlayerBullet beamPrefab;
    #endregion

    private PlayerBullet bul;
    private PlayerBullet bullet;
    private bool beam = false;

    // InvokeRepeating Shoot() method which shoots player bullets
    void Start()
    {
        InvokeRepeating("Shoot", 0f, firingRate);
    }

    private void Shoot()
    {
        if (beam)
        {
            bullet = Instantiate(beamPrefab);
        }
        else
        {
            bullet = Instantiate(bulletPrefab);
        }
        bullet.transform.position = transform.position;
        var b = transform.position.y + 0.8f;
        bullet.transform.position = new Vector2(transform.position.x, b);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed;
    }

    public void ActivateBeam()
    {
        beam = true;
        Invoke("DeActivateBeam", 4f);
    }

    public void DeActivateBeam()
    {
        beam = false;
    }

        public void IncreaseFiringRate()
    {
        firingRate = firingRate * 0.5f;
        bulletSpeed *= 2;
        InvokeRepeating("Shoot", 0f, firingRate);
        Invoke("DecreaseFiringRate", 6);
    }

    private void DecreaseFiringRate()
    {
        firingRate *= 2;
        bulletSpeed /= 2;
        CancelInvoke();
        InvokeRepeating("Shoot", 0f, firingRate);
    }

}
