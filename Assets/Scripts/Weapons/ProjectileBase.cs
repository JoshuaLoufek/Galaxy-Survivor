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
    public float projectileSpeed;
    public float aoe;
    public float attackDuration;
    public float critChance;
    public float critDamage;

    public float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        lifespan = 0f;
        attackDuration = 999f; // temporary attack duration until the real one is initialized
    }

    // Update is called once per frame
    public virtual void Update()
    {
        MoveProjectile();

        // Tracks time until the destruction of the object
        lifespan += Time.deltaTime;
        if (lifespan >= attackDuration) Destroy(gameObject);
    }

    // This will be called form the weapon attack script
    public virtual void InitializeProjectile(Transform origin, Vector2 fireDirection, WeaponBase weaponBase)
    {
        // Center the bullet on the player to start
        transform.position = origin.position;
        
        // Set the direction the projectile will travel in
        direction = new Vector2(fireDirection.x, fireDirection.y);

        weapon = weaponBase;
        
        // Set the stats up. We don't want a fired projectile to change behvaior mid flight, so set up and use projectile specific variables instead
        SetProjectileStats(weaponBase.currentWeaponStats);
    }

    public void SetProjectileStats(WeaponStats currentWeaponStats)
    {
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
