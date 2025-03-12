using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoWeapon : WeaponBase
{
    [SerializeField] GameObject yoyoPrefab;

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
        GameObject shotBullet = Instantiate(yoyoPrefab);
        // Set spawn position to be centered on the player
        shotBullet.transform.position = transform.position;
        // Set the travel direction and rotate the bullet to match
        shotBullet.GetComponent<YoyoProjectile>().InitializeProjectile
            (playerTransform, playerController.lastHorizontalVector, playerController.lastVerticalVector, currentWeaponStats.damage);

        // Sets the projectile to be a child of this weapon
        // projectile.transform.parent = transform; // This line was causing wonky projectile behavior for some weapons (laser gun)
    }
}
