using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpiralOrbitalProjectile : ProjectileBase
{
    float radius; 
    float moveSpeed;
    float currentAngle;
    float startTime;

    public float initialMoveSpeed;
    public float expansionSpeed;
    public float revolutionSpeed;

    Transform playerTransform;

    private void Awake()
    {
        radius = 1f;
        currentAngle = 0f;
        moveSpeed = initialMoveSpeed;
        startTime = Time.time;
    }

    public override void InitializeProjectile(Transform origin, Vector2 worthless, WeaponBase weaponBase)
    {
        playerTransform = origin;
        transform.position = new Vector2(origin.position.x, origin.position.y);
        
        weapon = weaponBase;

        SetProjectileStats(weaponBase.currentWeaponStats);
    }

    public override void MoveProjectile() // constant revolution speed
    {
        // Determine the position to move to
        float xPos = playerTransform.position.x + (radius * Mathf.Sin((Time.time - startTime) * revolutionSpeed));
        float yPos = playerTransform.position.y + (radius * Mathf.Cos((Time.time - startTime) * revolutionSpeed));

        // Move the projectile
        transform.position = new Vector2(xPos, yPos);

        // Update the radius for the next update
        radius += expansionSpeed * Time.deltaTime;
    }
}
