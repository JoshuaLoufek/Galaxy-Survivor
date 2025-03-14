using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

// This is the weapon base class that all other player weapons will inherit from.
public abstract class WeaponBase : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    public PlayerController playerController;
    public Transform playerTransform;
    public WeaponBase thisWeapon;

    public WeaponData defaultWeaponData; // This caries the DEFAULT weapon stats

    public PlayerStats playerStats; // This carries the PLAYER BUFFS
    // I need an object here to cover the current weapon upgrades
    // I need an object here to cover player's held items

    public WeaponStats currentWeaponStats; // The CURRENT weapon stats 

    public float attackTimer;

    // INITIALIZATION FUNCTIONS ==================================================================================

    public virtual void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        playerTransform = GetComponentInParent<Transform>();
        thisWeapon = GetComponent<WeaponBase>();
    }

    public virtual void Start()
    {
        attackTimer = 1f; // Ensures that the update function won't fire until the Initialize function runs
    }

    // Called from the weapon manager after the weapon is instantiated
    public virtual void InitializeWeaponData(WeaponData wd)
    {
        defaultWeaponData = wd;
        currentWeaponStats = new WeaponStats();

        playerStats = GetComponentInParent<PlayerStats>();
        // get weapon upgrades component
        // get held items component

        CalculateAllStats();
        attackTimer = currentWeaponStats.timeToAttack;
    }

    // UPDATE FUNCTION ===========================================================================================

    // Inherit the update method as-is for standard attacking behavior on a timer
    // Override it for unique functionality (i.e. laser beam)
    public virtual void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0f)
        {
            Attack();
            attackTimer = currentWeaponStats.timeToAttack;
        }
    }

    // ABSTRACT FUNCTIONS ========================================================================================

    // Each weapon will implement it's own unique attack method
    public abstract void Attack();

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    public void UpgradeWeapon(UpgradeData upgradeData)
    {
        currentWeaponStats.SumStats(upgradeData.weaponUpgradeStats);
    }

    // STATISTIC CALCULATOR FUNCTIONS ============================================================================
        // This is where the final, usable versions of each stat are calculated.
        // They will be applied in the functions that are above.
    
    public void CalculateAllStats()
    {
        CalculateDamage();
        CalculatePierce();
        CalculateTimeToAttack();
        CalculateProjectileSpeed();
        CalculateAOE();
        CalculateExtraAttacks();
        CalculateAttackDuration();
        CalculateCritChance();
        CalculateCritDamage();
        Debug.Log("Finished all weapon calculations successfully!");
    }

    public virtual void CalculateDamage()
    {
        currentWeaponStats.damage = defaultWeaponData.stats.damage * (1
            + playerStats.damage
            // + weaponBuffs
            // + itemBuffs
            );
        print("Current Damage Calculated: " + currentWeaponStats.damage);
    }
    
    public virtual void CalculatePierce()
    {
        currentWeaponStats.pierce = defaultWeaponData.stats.pierce * (1
            + playerStats.pierce
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateTimeToAttack() // NEEDS A UNIQUE FORMULA!!!!! 
    {   // All the other versions of this stat are "attack speed" not "time to attack" so it needs to be divided instead
        currentWeaponStats.timeToAttack = defaultWeaponData.stats.timeToAttack;
        Debug.Log("Time To Attack Set: " + currentWeaponStats.timeToAttack);
        // currentWeaponStats.timeToAttack = defaultWeaponData.stats.timeToAttack / (1
        // + playerStats.attackSpeed
        // + weaponBuffs
        // + itemBuffs
        //    );
    }

    public virtual void CalculateProjectileSpeed()
    {
        currentWeaponStats.projectileSpeed = defaultWeaponData.stats.projectileSpeed * (1
            + playerStats.projectileSpeed
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateAOE()
    {
        currentWeaponStats.aoe = defaultWeaponData.stats.aoe * (1
            + playerStats.aoe
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateExtraAttacks()
    {
        currentWeaponStats.extraAttacks = defaultWeaponData.stats.extraAttacks * (1
            + playerStats.extraAttacks
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateAttackDuration()
    {
        currentWeaponStats.attackDuration = defaultWeaponData.stats.attackDuration * (1
            + playerStats.attackDuration
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateCritChance()
    {
        currentWeaponStats.critChance = defaultWeaponData.stats.critChance * (1
            + playerStats.critChance
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateCritDamage()
    {
        currentWeaponStats.critDamage = defaultWeaponData.stats.critDamage * (1
            + playerStats.critDamage
            // + weaponBuffs
            // + itemBuffs
            );
    }
    
}
