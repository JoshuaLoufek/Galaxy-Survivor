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
    public float extraAttacks;
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

    // This will be called from the weapon attack script
    public virtual void InitializeProjectile(Transform origin, Vector2 fireDirection, WeaponBase weaponBase)
    {
        // Set the direction the projectile was fired in (might not always be relevant)
        direction = new Vector2(fireDirection.x, fireDirection.y);

        // Get a reference to the weapon for the purpose of showing damage numbers
        weapon = weaponBase;
        
        // Set the stats up. We don't want a fired projectile to change behvaior mid flight, so set up and use projectile specific variables instead
        SetProjectileStats(weaponBase.currentWeaponStats);

        // Increase projectile size in accordance with the AOE stat
        IncreaseProjectileSize();

        // Run any initialization unique to the projectile
        UniqueInitialization(origin, fireDirection);
    }

    public abstract void UniqueInitialization(Transform origin, Vector2 fireDirection);

    public void SetProjectileStats(WeaponStats currentWeaponStats)
    {
        damage = currentWeaponStats.damage; // implemented (DamageEnemy)
        pierce = currentWeaponStats.pierce; // implemented (DamageEnemy)
        projectileSpeed = currentWeaponStats.projectileSpeed; // implemented (MoveProjectile)
        aoe = currentWeaponStats.aoe; // not
        extraAttacks = currentWeaponStats.extraAttacks; // not
        attackDuration = currentWeaponStats.attackDuration; // implemented (Update)
        critChance = currentWeaponStats.critChance; // not
        critDamage = currentWeaponStats.critDamage; // not
    }
    
    public void IncreaseProjectileSize()
    {
        Vector3 currentSize = transform.localScale;
        transform.localScale = new Vector3(currentSize.x * Mathf.Sqrt(aoe), currentSize.y * Mathf.Sqrt(aoe), currentSize.z);

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
