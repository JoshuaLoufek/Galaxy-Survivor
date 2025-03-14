using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralOrbitalWeapon : WeaponBase
{
    [SerializeField] GameObject spiralOrbPrefab;

    public override void Attack()
    {
        // Create bullet object
        GameObject shotBullet = Instantiate(spiralOrbPrefab);
        // Set spawn position to be centered on the player
        shotBullet.transform.position = transform.position;
        // Set the direction the projectile will be fired in
        Vector2 fireDirection = new Vector2(playerController.lastHorizontalVector, playerController.lastVerticalVector);
        // Set the travel direction and rotate the bullet to match
        shotBullet.GetComponent<ProjectileBase>().InitializeProjectile(playerTransform, fireDirection, thisWeapon);
    }
}
