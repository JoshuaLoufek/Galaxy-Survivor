using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamV2 : WeaponBase
{
    public LayerMask obstacleLayers;
    public LayerMask hitLayers;

    [SerializeField] private float distance = 10f;
    public Transform firePoint;
    public Transform firePointRotator;
    public LineRenderer attackingLaserRenderer;
    public LineRenderer targetingLaserRenderer;

    // Variables that are critical to the function of the laser
    Boolean isFiring = false; // represents if the laser is currently firing
    float cooldownTimer = 0f;
    float durationTimer = 0f;
    Vector2 laserDirection;
    Vector2 laserStart;
    Vector2 laserEnd;

    // Variables I can use to adjust the behavior of the laser
    public float radius = .5f;
    // public int damage = 1; // REPLACED BY WEAPONSTATS.DAMAGE
    // public float cooldown = 3f; // REPLACED BY WEAPONSTATS.TIMETOATTACK
    public float duration = 2f;
    public float rotationSpeed = 30f; // Represents degrees per second

    // override the default weapon behavior
    public override void Update()
    {
        if (playerController.aiming) RotateFirePoint(); // If the player is aiming, rotate the fire point.
        UpdateLaserPositionVariables(); // Update all the variables that are needed to properly display the laser.

        if (isFiring)
        {
            // The laser fires for a certain duration
            if (durationTimer > duration) // if the time is up, the laser stops firing and the timer resets
            {
                Attack();
                isFiring = false;
                attackingLaserRenderer.enabled = false;
                durationTimer = 0f;
            } 
            else // Otherwise the laser keeps firing (or starts if this is the first time here).
            {
                attackingLaserRenderer.enabled = true;
                Attack();
                durationTimer += Time.deltaTime;
            }
            
        } else
        {
            // There is a cooldown period between shots
            if (cooldownTimer > currentWeaponStats.timeToAttack) // if the time is up, the laser starts firing again and the timer resets
            {
                ShootTargetingLaser();
                isFiring = true;
                cooldownTimer = 0f;
                targetingLaserRenderer.enabled = false;
            }
            else // Otherwise the laser stays on cooldown
            {
                targetingLaserRenderer.enabled = true;
                ShootTargetingLaser();
                cooldownTimer += Time.deltaTime;
            }
        }
    }

    public override void Attack()
    {
        RaycastHit2D hit_obstacle; // Gets the first valid obstacle hit (if any)
        RaycastHit2D[] hit_enemies; // Does need to be an array. Want to return all enemies hit by the beam.

        // 1. Cast a beam in the intended direction of the laser. Will return the first valid obstacle hit if there is one.
        hit_obstacle = Physics2D.CircleCast(laserStart, radius, laserDirection, distance, obstacleLayers);

        // 2. Check for enemies hit and draw the beam.
        if (hit_obstacle.collider != null) // Used when an obstacle was hit
        {
            Draw2DRay(laserStart, hit_obstacle.point, attackingLaserRenderer);
            hit_enemies = Physics2D.CircleCastAll(laserStart, radius, hit_obstacle.point, hitLayers);

        }
        else // Used when no obstacle was hit
        {
            Draw2DRay(laserStart, laserEnd, attackingLaserRenderer);
            hit_enemies = Physics2D.CircleCastAll(laserStart, radius, laserDirection, distance, hitLayers);
        }

        // 3. Damage any enemies hit by the beam.
        foreach (RaycastHit2D hit in hit_enemies)
        {
            // Grabs the enemy combat script
            IDamageable enemy = hit.collider.GetComponent<IDamageable>();

            // Verifies that the found object was an enemy
            if (enemy != null)
            {
                enemy.TakeDamage(currentWeaponStats.damage); // Damages the enemy
            }
            // new line
        }
    }


    void ShootTargetingLaser()
    {
        RaycastHit2D hit_obstacle  = Physics2D.CircleCast(laserStart, radius, laserDirection, distance, obstacleLayers);
        if (hit_obstacle.collider != null) Draw2DRay(laserStart, hit_obstacle.point, targetingLaserRenderer);
        else Draw2DRay(laserStart, laserEnd, targetingLaserRenderer);
    }


    private void RotateFirePoint()
    {
        float appliedRotation;

        // Get the angle the player wants to rotate the laser to and the current firing angle in degrees.
        float intendedFireAngle = (Mathf.Atan2(playerController.lastVerticalVector, playerController.lastHorizontalVector)) * Mathf.Rad2Deg;
        float currentFireAngle = firePointRotator.transform.rotation.eulerAngles.z;

        // Convert to a 0-360 degree scale (from -180 to 180)
        intendedFireAngle = Mod(intendedFireAngle, 360f);
        currentFireAngle = Mod(currentFireAngle, 360f);

        // Determine which rotation direction is shorter
        float cwORccw = AngleDifference(intendedFireAngle, currentFireAngle);

        // Set the applied rotation according to which angle is shorter
        if (cwORccw >= 0) appliedRotation = rotationSpeed; // Set the roation to be clockwise (positive)
        else appliedRotation = -1 * rotationSpeed; // Set the rotation to be counterclockwise (negative)

        // Apply the rotation
        firePointRotator.transform.Rotate(0f, 0f, appliedRotation * Time.deltaTime);
    }

    // This should be called AFTER any rotations or movements to the fire point, but BEFORE the laser is drawn and enemies are hit
    // These variables are necessary to properly display the laser beam
    private void UpdateLaserPositionVariables()
    {
        float x = Mathf.Cos(firePointRotator.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        float y = Mathf.Sin(firePointRotator.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        laserStart = new Vector2(firePoint.position.x, firePoint.position.y);
        laserEnd = new Vector2(firePoint.position.x, firePoint.position.y) + (laserDirection * distance);
        laserDirection = new Vector2(x, y);
    }

    float AngleDifference(float angle1, float angle2)
    {
        //double diff = (angle2 - angle1 + 180) % 360 - 180;
        float diff = Mod((Mod(angle1, 360) - Mod(angle2, 360) + 180f), 360) - 180f;
        return diff < -180 ? diff + 360 : diff;
    }

    float Mod(float a, float b)
    {
        return (a % b + b) % b;
    }

    void Draw2DRay(Vector2 start, Vector2 end, LineRenderer lineRenderer)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
