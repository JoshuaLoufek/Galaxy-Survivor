using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectileWeapon : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    PlayerController playerControlSystem;
    Transform playerTransform;

    [SerializeField] float timeToAttack = 3f;
    float timer = 0f;

    void Start()
    {
        playerControlSystem = GetComponentInParent<PlayerController>();
        playerTransform = GetComponentInParent<Transform>();
    }

    void Update()
    {
        AttemptAttack();
    }

    void AttemptAttack()
    {
        if (timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        SpawnProjectile();
    }

    void SpawnProjectile()
    {
        // Create the projectile
        GameObject projectile = Instantiate(projectilePrefab);

        // Initialize the projectile's variables
        float x = playerControlSystem.lastHorizontalVector;
        float y = playerControlSystem.lastVerticalVector;
        projectile.GetComponent<IProjectile>().InitializeProjectile(playerTransform, x, y);

        // Sets the projectile to be a child of this weapon
        // projectile.transform.parent = transform; // This line was causing wonky projectile behavior for some weapons (laser gun)
    }
}
