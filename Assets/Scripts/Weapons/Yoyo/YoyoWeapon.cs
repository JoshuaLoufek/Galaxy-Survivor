using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class YoyoWeapon : WeaponBase
{
    [SerializeField] GameObject yoyoPrefab;

    public override void Attack()
    {
        // Create bullet object
        GameObject shotBullet = Instantiate(yoyoPrefab);

        // Set spawn position to be centered on the player
        shotBullet.transform.position = transform.position;

        // Set the direction the projectile will be fired in
        Vector2 fireDirection = new Vector2(playerController.lastHorizontalVector, playerController.lastVerticalVector);

        // Set the travel direction and rotate the bullet to match
        shotBullet.GetComponent<YoyoProjectile>().InitializeProjectile(playerTransform, fireDirection, thisWeapon);
    }
}
