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

    public WeaponData defaultWeaponData; // This caries the DEFAULT weapon stats. Also carries important reference information.
    public PlayerStats playerStats; // This carries the PLAYER BUFFS
    private WeaponStats weaponUpgrades; // This carries the WEAPON BUFFS
    // I need an object here to cover the player's held items
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
        attackTimer = 1f; // Ensures that the update function won't fire the weapon until the Initialize function runs
    }

    // Called from the weapon manager after the weapon is instantiated
    public virtual void InitializeWeaponData(WeaponData wd)
    {
        defaultWeaponData = wd; // Get the default stats and data references for this weapon

        playerStats = GetComponentInParent<PlayerStats>(); // get a reference the player stats component
        weaponUpgrades = new WeaponStats(); // Create a new WeaponStats object to hold the upgrade buffs
        // get held items component

        currentWeaponStats = new WeaponStats(); // Create an new WeaponStats object to hold the curret stats of the weapon
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

    // HELPER FUNCTIONS ==========================================================================================

    public void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    // Called when a weapon upgrade is collected. Adds the percentage buffs to the weaponUpgrades object, then calls the stat calculator.
    public void UpgradeWeapon(UpgradeData upgradeData)
    {
        weaponUpgrades.AddBuffs(upgradeData.weaponUpgradeStats);
        CalculateAllStats();
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
            + weaponUpgrades.damage
            // + itemBuffs
            );

        Debug.Log("Current Damage Calculated: " + currentWeaponStats.damage);
    }
    
    public virtual void CalculatePierce()
    {
        currentWeaponStats.pierce = defaultWeaponData.stats.pierce * (1
            + playerStats.pierce
            + weaponUpgrades.pierce
            // + itemBuffs
            );
    }

    public virtual void CalculateTimeToAttack() // NEEDS A UNIQUE FORMULA!!!!! 
    {   // All the other versions of this stat are "attack speed" not "time to attack" so it needs to be divided instead
        currentWeaponStats.timeToAttack = defaultWeaponData.stats.timeToAttack / (1
            + playerStats.attackSpeed
            + weaponUpgrades.timeToAttack // this one says time to attack. When done as an upgrade it represents attack speed instead. (see line 20 of WeaponData)
            );

        Debug.Log("Time To Attack Set: " + currentWeaponStats.timeToAttack);
    }

    public virtual void CalculateProjectileSpeed()
    {
        currentWeaponStats.projectileSpeed = defaultWeaponData.stats.projectileSpeed * (1
            + playerStats.projectileSpeed
            + weaponUpgrades.projectileSpeed
            // + itemBuffs
            );
    }

    public virtual void CalculateAOE()
    {
        currentWeaponStats.aoe = defaultWeaponData.stats.aoe * (1
            + playerStats.aoe
            + weaponUpgrades.aoe
            // + itemBuffs
            );
        Debug.Log("AOE set: " + currentWeaponStats.aoe);
    }

    public virtual void CalculateExtraAttacks()
    {
        currentWeaponStats.extraAttacks = defaultWeaponData.stats.extraAttacks * (1
            + playerStats.extraAttacks
            + weaponUpgrades.extraAttacks
            // + itemBuffs
            );
    }

    public virtual void CalculateAttackDuration()
    {
        currentWeaponStats.attackDuration = defaultWeaponData.stats.attackDuration * (1
            + playerStats.attackDuration
            + weaponUpgrades.attackDuration
            // + itemBuffs
            );
    }

    public virtual void CalculateCritChance()
    {
        currentWeaponStats.critChance = defaultWeaponData.stats.critChance * (1
            + playerStats.critChance
            + weaponUpgrades.critChance
            // + itemBuffs
            );
    }

    public virtual void CalculateCritDamage()
    {
        currentWeaponStats.critDamage = defaultWeaponData.stats.critDamage * (1
            + playerStats.critDamage
            + weaponUpgrades.critDamage
            // + itemBuffs
            );
    }
}
