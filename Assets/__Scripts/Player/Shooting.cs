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

    private PlayerBullet bullet;
    private PlayerBullet beam;

    // InvokeRepeating Shoot() method which shoots player bullets
    void Start()
    {
        InvokeRepeating("Shoot", 0f, firingRate);
    }

    private void Shoot()
    {
        Bullet();
        bullet.transform.position = new Vector2(transform.position.x, transform.position.y + 0.8f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>(); 
        rb.velocity = Vector2.up * bulletSpeed;
    }

    // If beam is activated, bullet instantiated to beam prefab
    private void Bullet()
    {
        if (beam)
        {
            bullet = Instantiate(beamPrefab);
        }
        else
        {
            bullet = Instantiate(bulletPrefab);
        }
    }

    // Activate beam
    public void ActivateBeam()
    {
        beam = Instantiate(beamPrefab);
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
