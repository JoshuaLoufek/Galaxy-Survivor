using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages weapons on the player. Allows player to add and upgrade weapons.
public class PlayerWeapons : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    // Player Scripts
    Level level;
    
    // References to other objects
    [SerializeField] Transform weaponObjectsContainer; // stores all instantiated weapons in the hierarchy
    [SerializeField] WeaponData startingWeapon;
    List<WeaponBase> aquiredWeapons;

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        level = GetComponent<Level>();
        aquiredWeapons = new List<WeaponBase>();
    }

    private void Start()
    {
        AddWeapon(startingWeapon);
    }

    // WEAPON MANAGEMENT FUNCTIONS ===============================================================================

    public void AddWeapon(WeaponData weaponData)
    {
        GameObject newWeapon = Instantiate(weaponData.weaponPrefab, weaponObjectsContainer);
        WeaponBase weaponBase = newWeapon.GetComponent<WeaponBase>();

        weaponBase.InitializeWeaponData(weaponData);
        aquiredWeapons.Add(weaponBase);

        level.AddToListOfAvailableUpgrades(weaponData.upgrades);
    }

    internal void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = aquiredWeapons.Find(wd => wd.defaultWeaponData == upgradeData.weaponData);
        weaponToUpgrade.Upgrade(upgradeData);
    }

    // STATISTIC CALCULATOR FUNCTIONS =====================================================================================
        // This is where the final, usable versions of each stat are calculated.
        // They will be applied in the functions that are above.

    // This script is the exception to how the player controller and player health functions calculate their stats
    // This script needs to pass the updates to all the weapons the player has aquired

    public void UpdateWeaponStats()
    {
        foreach (WeaponBase weapon in aquiredWeapons)
        {

        }
    }

}