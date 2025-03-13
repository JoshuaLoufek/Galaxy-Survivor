using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    public Rigidbody2D myRB;
    public WeaponBase weapon;

    public Vector3 direction;

    public float damage;
    public float pierce;
    // public float attackSpeed; // this stat doesn't matter to a projectile
    public float projectileSpeed;
    public float aoe;
    // public float extraAttacks; // this stat doesn't matter to a projectile
    public float attackDuration;
    public float critChance;
    public float critDamage;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    // This will be called form the weapon attack script
    public virtual void InitializeProjectile(Transform origin, Vector2 fireDirection, WeaponStats currentWeaponStats)
    {
        // Center the bullet on the player to start
        transform.position = origin.position;
        
        // Set the direction the projectile will travel in
        direction = new Vector3(fireDirection.x, fireDirection.y);
        
        // Gets a reference to the weapon that fired the projectile
        // weapon = weaponBase;
        
        // Set the stats up. We don't want a fired projectile to change behvaior mid flight, so set up and use projectile specific variables instead
        damage = currentWeaponStats.damage;
        pierce = currentWeaponStats.pierce;
        projectileSpeed = currentWeaponStats.projectileSpeed;
        aoe = currentWeaponStats.aoe;
        attackDuration = currentWeaponStats.attackDuration;
        critChance = currentWeaponStats.critChance;
        critDamage = currentWeaponStats.critDamage;
    }

    public abstract void MoveProjectile();

    public virtual void OnTriggerEnter2D(Collider2D collision)
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
