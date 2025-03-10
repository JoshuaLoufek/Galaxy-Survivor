using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages weapons on the player. Allows player to add and upgrade weapons.
public class WeaponManager : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    // Player Scripts
    Player playerStats;
    Level level;
    
    // Player Stats (Player bonuses that trickle down to all their weapons)
    public float playerDamage;
    public float playerPierce;
    public float playerAttackSpeed;
    public float playerProjectileSpeed;
    public float playerAOE;
    public float playerBonusAttacks;
    public float playerAttackDuration;
    
    // References to other objects
    [SerializeField] Transform weaponObjectsContainer; // stores all instantiated weapons in the hierarchy
    [SerializeField] WeaponData startingWeapon;
    List<WeaponBase> aquiredWeapons;

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        playerStats = GetComponent<Player>();
        UpdateStats();

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

    // PLAYER STATS FUNCTIONS ====================================================================================

    private void UpdateStats() // Updates all the combat stats in this script using the current player core stats
    {
        CoreStats coreStats = playerStats.coreStats;

        setDamage(coreStats.power);
        setPierce(coreStats.power);
        setAttackSpeed(coreStats.speed);
        setProjectileSpeed(coreStats.speed);
        setAOE(coreStats.amplify);
        setBonusAttacks(coreStats.amplify);
        setAttackDuration(coreStats.capacity);
    }

    private void setDamage(int power)
    {
        // 10% damage bonus per level
        playerDamage = 0.1f * power;
    }

    private void setPierce(int power)
    {
        // 50% pierce bonus per level
        playerPierce = 0.5f * power;

    }

    private void setAttackSpeed(int speed)
    {
        // 10% bonus attack speed per level
        playerAttackSpeed = 0.1f * speed;
    }

    private void setProjectileSpeed(int speed)
    {
        // 10% bonus projectile speed per level
        playerProjectileSpeed = 0.1f * speed;
    }

    private void setAOE(int amplify)
    {
        // 20% bonus aoe per level
        playerAOE = 0.2f * amplify;
    }

    private void setBonusAttacks(int amplify)
    {
        // 10% bonus attacks per level
        playerBonusAttacks = 0.1f * amplify;
    }

    private void setAttackDuration(int capacity)
    {
        // 10% extra attack duration per level
        playerAttackDuration = 0.1f * capacity;
    }
}