using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Level : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    int level = 1;
    int experience = 0;
    [SerializeField] StatusBar EXPBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    [SerializeField] List<UpgradeData> availableUpgrades; // Upgrades the player can currently choose from when they level up.
    [SerializeField] List<UpgradeData> acquiredUpgrades; // Upgrades the player has already recieved.
    List<UpgradeData> futureUpgrades; // Upgrades that the player isn't high enough level to be put in rotation yet
    List<UpgradeData> selectedUpgrades; // Upgrades that will be presented to the player this level up

    [SerializeField] int upgradesPerLevel; // represents how many upgrades the player should be able to choose from each level

    PlayerWeapons weaponManager;

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        weaponManager = GetComponent<PlayerWeapons>();
        // availableUpgrades = new List<UpgradeData>();// UNCOMMENT ME WHENEVER WE'RE READY TO ADD IN EVERYTHING DYNAMICALLY
        acquiredUpgrades = new List<UpgradeData>();
        futureUpgrades = new List<UpgradeData>();
        selectedUpgrades = new List<UpgradeData>();
    }

    private void Start()
    {
        EXPBar.SetState(experience, TO_LEVEL_UP);
        EXPBar.SetLevelText(level);
    }

    // EXPERIENCE & LEVEL UP =====================================================================================

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        EXPBar.SetState(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // Check if any future upgrades are now level appropriate for the player.
        foreach (UpgradeData upgrade in futureUpgrades) { if (upgrade.level <= level) { availableUpgrades.Add(upgrade); } }

        // Chooses which upgrades are available this level and presents them to the player
        selectedUpgrades.Clear(); // Clear the upgrades that were selected last level up
        selectedUpgrades.AddRange(GetUpgrades(upgradesPerLevel)); // Get the upgrades to present to the player and add them to the list
        upgradePanelManager.OpenPanel(selectedUpgrades); // Open the upgrade menu

        // Subtract the experience, increase the player's level, and update the UI
        experience -= TO_LEVEL_UP;
        level += 1;
        EXPBar.SetLevelText(level);
    }

    // UPGRADE & UNLOCKS =========================================================================================

    // This function chooses which upgrades to present to the player when they level up.
    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        // if there aren't enough upgrades left, only get what we can
        if (count > availableUpgrades.Count) { count = availableUpgrades.Count; }

        for (int i = 0; i < count; i++)
        {
            // Get an upgrade from the list of available upgrades
            UpgradeData upgrade = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
            // Add it to the list that be presented to the player
            upgradeList.Add(upgrade);
            // Remove it from the list of available upgrades so that it won't be chosen twice to be presented to the player
            availableUpgrades.Remove(upgrade);
        }

        // Add the upgrades chosen to be presented back to the available upgrade list 
        availableUpgrades.AddRange(upgradeList);

        return upgradeList;
    }

    // This function implements the upgrade chosen by the player. It is called from the UpgradePanelManager script.
    internal void Upgrade(int selectedUpgradeID)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeID];
        if (acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUnlock:
                weaponManager.AddWeapon(upgradeData);
                break;
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                break;
            case UpgradeType.ItemUnlock:
                break;
            case UpgradeType.ItemUpgrade:
                break;
            case UpgradeType.RelicUnlock:
                break;
            case UpgradeType.RelicUpgrade:
                break;
        }
        // Add this upgrade to the list of aquired upgrades
        acquiredUpgrades.Add(upgradeData);
        // Remove it from the list of available upgrades
        availableUpgrades.Remove(upgradeData);
    }

    internal void AddToListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        // Upgrades that are too high of a level aren't added to the available upgrade pool. Instead they're set aside in the future upgrade pool.
        foreach (UpgradeData upgradeData in upgradesToAdd)
        {
            if (upgradeData.level <= level) { availableUpgrades.Add(upgradeData); } 
            else { futureUpgrades.Add(upgradeData); }
        }
    }

    internal void RemoveFromListOfAvailableUpgrades(List<UpgradeData> removeWhenChosen)
    {
        // Removes upgrades that are mutually exclusive with the chosen upgrade.
        foreach (UpgradeData upgradeData in removeWhenChosen)
        {
            if (availableUpgrades.Contains(upgradeData)) { availableUpgrades.Remove(upgradeData); }
        }
    }
}
