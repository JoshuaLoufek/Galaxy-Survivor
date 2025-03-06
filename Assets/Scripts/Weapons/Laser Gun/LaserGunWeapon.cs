using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunWeapon : WeaponBase
{
    [SerializeField] GameObject bulletPrefab;

    PlayerController playerController;
    Transform playerTransform;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerTransform = GetComponentInParent<Transform>();
    }

    public override void Attack()
    {
        // Create bullet object
        GameObject shotBullet = Instantiate(bulletPrefab);
        // Set spawn position to be centered on the player
        shotBullet.transform.position = transform.position;
        // Set the travel direction and rotate the bullet to match
        shotBullet.GetComponent<LaserBulletProjectile>().InitializeProjectile
            (playerTransform, playerController.lastHorizontalVector, playerController.lastVerticalVector, weaponStats.damage, this);
    }
}
