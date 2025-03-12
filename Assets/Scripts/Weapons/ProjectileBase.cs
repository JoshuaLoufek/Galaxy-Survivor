using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    Rigidbody2D myRB;
    WeaponBase weapon;

    Vector3 direction;

    float damage;
    float pierce;
    // float attackSpeed;
    float projectileSpeed;
    float aoe;
    // float extraAttacks;
    float attackDuration;
    float critChance;
    float critDamage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    public virtual void InitializeProjectile(Transform origin, Vector2 fireDirection, WeaponBase weaponBase)
    {
        // Center the bullet on the player to start
        transform.position = origin.position;
        // Set the direction the projectile will travel in
        direction = new Vector3(fireDirection.x, fireDirection.y);
        // Gets a reference to the weapon that fired the projectile
        weapon = weaponBase; // can use this as a stand in for the weapon's stats OR use this to set up the stats within the projectile itself
    }

    public abstract void MoveProjectile();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageEnemy(collision);
    }

    public virtual void DamageEnemy(Collider2D collision)
    {
        // Grabs the enemy combat script
        IDamageable enemy = collision.GetComponent<IDamageable>();

        // Verifies that the found object was an enemy
        if (enemy != null)
        {
            weapon.PostDamage((int)damage, collision.transform.position);
            enemy.TakeDamage((int)damage); // Damages the enemy
            pierce -= 1; // Reduces the pierce counter
            if (pierce <= 0) Destroy(gameObject); // Checks if pierce hit zero and the bullet needs destroying
        }
    }
}
