using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserGunWeapon : WeaponBase
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float spread = 0.5f;
    [SerializeField] float distanceFromPlayer = 1f;

    public override void Attack()
    {
        for (int i=0; i < currentWeaponStats.extraAttacks; i++)
        {
            // Create bullet object
            GameObject shotBullet = Instantiate(bulletPrefab);

            // Set the bullet position
            shotBullet.transform.position = calculateBulletPosition(i);

            // Set the direction the projectile will be fired in
            Vector2 fireDirection = new Vector2(playerController.lastHorizontalVector, playerController.lastVerticalVector);

            // Set the travel direction and rotate the bullet to match
            shotBullet.GetComponent<ProjectileBase>().InitializeProjectile(playerTransform, fireDirection, thisWeapon);
        }
    }

    private Vector3 calculateBulletPosition(int i)
    {
        float farthestPositiveSpread = (spread * currentWeaponStats.extraAttacks) / 2; // get the distance of the line the projectiles will be spread across
        float currentSpread = farthestPositiveSpread - (i * spread); ; // Calculate how far on the line an individual projectile should be. Can be negative with a negative offset
        float D = Mathf.Sqrt(distanceFromPlayer * distanceFromPlayer + currentSpread * currentSpread);

        float aimAngle = Mathf.Atan(playerController.lastVerticalVector / playerController.lastHorizontalVector);
        float theta = Mathf.Atan(currentSpread / distanceFromPlayer);
        float alpha = aimAngle + theta;
        
        float x = D * Mathf.Cos(alpha);
        float y = D * Mathf.Sin(alpha);

        if (playerController.lastHorizontalVector > 0) return transform.position + new Vector3(x, y, 0);
        else return transform.position - new Vector3(x, y, 0);
    }
}
