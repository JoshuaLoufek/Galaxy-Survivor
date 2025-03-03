using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack = 1f;
    float timer = 0f;

    [SerializeField] GameObject bulletPrefab;

    PlayerController playerControlSystem;
    Transform playerTransform;

    private void Awake()
    {
        playerControlSystem = GetComponentInParent<PlayerController>();
        playerTransform = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        if(timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        // Create bullet object
        GameObject shotBullet = Instantiate(bulletPrefab);
        // Set spawn position to be centered on the player
        shotBullet.transform.position = transform.position;
        // Set the travel direction and rotate the bullet to match
        shotBullet.GetComponent<LaserBulletProjectile>().InitializeProjectile(playerTransform, playerControlSystem.lastHorizontalVector, playerControlSystem.lastVerticalVector);
    }

    public void SetAttackSpeed(float attackSpeed) // an attack speed of 5 (5 bullets/sec) translates to 0.2 time to attack (5 bullets/sec)
    {
        timeToAttack = 1/attackSpeed;
    }
}
