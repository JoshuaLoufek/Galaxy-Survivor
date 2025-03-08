using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages weapons on the player. Allows player to add and upgrade weapons.
public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer; // stores all instantiated weapons in the hierarchy

    [SerializeField] WeaponData startingWeapon;

    List<WeaponBase> aquiredWeapons;

    Level level;

    private void Awake()
    {
        level = GetComponent<Level>();
        aquiredWeapons = new List<WeaponBase>();
    }

    private void Start()
    {
        AddWeapon(startingWeapon);
    }

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
}
