using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the weapon base class that all other player weapons will inherit from.
public abstract class WeaponBase : MonoBehaviour
{
    // GLOBAL VARIABLES =============================================================================

    public WeaponData defaultWeaponData; // This caries the DEFAULT weapon stats

    public PlayerStats playerStats; // This carries the PLAYER BUFFS
    // I need an object here to cover the current weapon upgrades
    // I need an object here to cover player's held items

    public WeaponStats currentWeaponStats; // The CURRENT weapon stats 

    float timer;

    // INITIALIZATION FUNCTIONS =====================================================================

    public void Start()
    {
        timer = 1f;
    }

    // Uses the scriptable object that holds the default weapon stats
    // Called from the weapon manager after the weapon is created (instantiated)
    public virtual void InitializeWeaponData(WeaponData wd)
    {
        defaultWeaponData = wd;
        currentWeaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
    }

    // UPDATE FUNCTION ==============================================================================

    // Inherit the update method as-is for standard attacking behavior on a timer
    // Override it for unique functionality (i.e. laser beam)
    public virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            Attack();
            timer = currentWeaponStats.timeToAttack;
        }
    }

    // ABSTRACT FUNCTIONS ===========================================================================

    // Each weapon will implement it's own unique attack method
    public abstract void Attack();

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    // ===========================

    public void Upgrade(UpgradeData upgradeData)
    {
        currentWeaponStats.SumStats(upgradeData.weaponUpgradeStats);
    }

    // STATISTIC CALCULATOR FUNCTIONS ============================================================================
        // This is where the final, usable versions of each stat are calculated.
        // They will be applied in the functions that are above.

    public virtual void CalculateDamage()
    {
        currentWeaponStats.damage = defaultWeaponData.stats.damage * (1
            + (int)playerStats.damage
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculatePierce()
    {
        currentWeaponStats.pierce = defaultWeaponData.stats.pierce * (1
            + (int)playerStats.pierce
            // + weaponBuffs
            // + itemBuffs
            );
    }

    public virtual void CalculateTimeToAttack()
    {

    }

    public virtual void CalculateProjectileSpeed()
    {

    }

    public virtual void CalculateAOE()
    {

    }

    public virtual void CalculateExtraAttacks()
    {

    }

    public virtual void CalculateAttackDuration()
    {

    }

    public virtual void CalculateCritChance()
    {

    }

    public virtual void CalculateCritDamage()
    {

    }
}
