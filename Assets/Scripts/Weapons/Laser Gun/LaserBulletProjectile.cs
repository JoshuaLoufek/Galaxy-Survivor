using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletProjectile : ProjectileBase
{
    // INITIALIZATION FUNCTIONS =====================================================================

    public override void InitializeProjectile(Transform origin, Vector2 fireDirection, WeaponBase weaponBase)
    {
        transform.position = origin.position;

        direction = fireDirection;

        // Get a reference to the weapon that fired this projectile
        weapon = weaponBase;

        // Rotates the bullet so it's aligned properly
        float z = -90 + (Mathf.Atan2(fireDirection.y, fireDirection.x) * (180 / Mathf.PI));
        transform.localRotation = Quaternion.Euler(0, 0, z);

        SetProjectileStats(weaponBase.currentWeaponStats);

        IncreaseProjectileSize();
    }

    // HELPER FUNCTIONs =============================================================================

    public override void MoveProjectile()
    {
        myRB.velocity = direction * projectileSpeed;
        //transform.position += direction * speed * Time.deltaTime;
    }
}
