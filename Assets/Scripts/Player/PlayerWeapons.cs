using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Manages weapons on the player. Allows player to add and upgrade weapons.
public class PlayerWeapons : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    // Player Scripts
    Level level;
    
    // References to other objects
    [SerializeField] Transform weaponObjectsContainer; // stores all instantiated weapons in the hierarchy
    [SerializeField] UpgradeData startingWeaponUnlock;
    List<WeaponBase> aquiredWeapons;

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        level = GetComponent<Level>();
        aquiredWeapons = new List<WeaponBase>();
    }

    private void Start()
    {
        AddWeapon(startingWeaponUnlock);
    }

    // WEAPON MANAGEMENT FUNCTIONS ===============================================================================

    public void AddWeapon(UpgradeData upgradeData)
    {
        GameObject newWeapon = Instantiate(upgradeData.weaponData.weaponPrefab, weaponObjectsContainer);
        WeaponBase weaponBase = newWeapon.GetComponent<WeaponBase>();

        weaponBase.InitializeWeaponData(upgradeData.weaponData);
        aquiredWeapons.Add(weaponBase);

        level.AddToListOfAvailableUpgrades(upgradeData.upgradeUnlocks);
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = aquiredWeapons.Find(wd => wd.defaultWeaponData == upgradeData.weaponData); // grab a reference to the weapon we're upgrading
        weaponToUpgrade.UpgradeWeapon(upgradeData); // implement the upgrade
        level.AddToListOfAvailableUpgrades(upgradeData.upgradeUnlocks); // add the next (set of) upgrade(s) to the list
    }

    // STATISTIC CALCULATOR FUNCTIONS =====================================================================================
        // This is where the final, usable versions of each stat are calculated.
        // They will be applied in the functions that are above.

    // This script is the exception to how the player controller and player health functions calculate their stats
    // This script needs to call the stat calculator functions for each weapon the player has aquired
    
    // Needs to be called in each of the following scenarios:
        // The player's weapon relevant core stats get updated (implemented)
        // ???

    public void UpdateWeaponStats()
    {
        if (aquiredWeapons == null || !aquiredWeapons.Any()) return;
        foreach (WeaponBase weapon in aquiredWeapons)
        {
            weapon.CalculateAllStats();
        }
    }

}