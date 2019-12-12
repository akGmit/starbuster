using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Script implementeng Enemy shooting behaviour. 
/// Here are defined various enemy shooting types.
/// </summary>
public class EnemyShooting : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    private EnemyBullet bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float firingRate;
    #endregion

    private LevelSettings settings;
    private GameObject player;

    void Start()
    {
        settings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
        bulletSpeed = settings.EnemyBulletSpeed;
        firingRate = settings.EnemyFiringRate;
        player = GameObject.Find("Player");

        Shot();
    }

    // Call appropriate shooting bahaviour
    void Shot()
    {
        var tag = gameObject.tag;

        if (tag == "EnemyEasy")
        {
            InvokeRepeating("ShootStraightDown", 0f, firingRate);
        }
        else if (tag == "EnemySniper")
        {
            InvokeRepeating("ShootAtPlayer", 0f, firingRate);
        }
        else if (tag == "Boss")
        {
            InvokeRepeating("BossShooting", 0f, firingRate / 3);
        }else if (tag == "EnemyDestroyer")
        {
            InvokeRepeating("ChasingShot", 0f, firingRate);
        }
    }

    /// <summary>
    /// Boss shooting behaviour implmementation.
    /// </summary>
    private void BossShooting()
    {
        var target = TargetDirection();
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = target * bulletSpeed * 2f;

        bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x + 0.2f, target.y) * bulletSpeed * 1.5f;

        bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(target.x - 0.2f, TargetDirection().y) * bulletSpeed * 1.5f;
    }

    private void ChasingShot()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
    }

    // Shooting towards target position
    private void ShootAtPlayer()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = TargetDirection() * bulletSpeed;
    }

    // Straight down shot
    private void ShootStraightDown()
    {
        EnemyBullet bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * bulletSpeed;
    }

    private Vector3 TargetDirection()
    {
        return (player.transform.position - transform.position).normalized;
    }
}
