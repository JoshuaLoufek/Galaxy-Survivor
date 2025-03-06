using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the weapon base class that all other player weapons will inherit from.
public abstract class WeaponBase : MonoBehaviour
{
    // GLOBAL VARIABLES =============================================================================

    public WeaponData defaultWeaponData; // This caries the DEFAULT weapon stats
    public WeaponStats weaponStats; // The CURRENT weapon stats

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
        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.timeToAttack);
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
            timer = weaponStats.timeToAttack;
        }
    }

    // ABSTRACT FUNCTIONS ===========================================================================

    // Each weapon will implement it's own unique attack method
    public abstract void Attack();

    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }
}
