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
    List<UpgradeData> selectedUpgrades;

    [SerializeField] int upgradesPerLevel; // represents how many upgrades the player should be able to choose from each level

    WeaponManager weaponManager;

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
        weaponManager = GetComponent<WeaponManager>();
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
        if (selectedUpgrades == null) { selectedUpgrades  = new List<UpgradeData>(); }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(upgradesPerLevel));

        upgradePanelManager.OpenPanel(selectedUpgrades);
        experience -= TO_LEVEL_UP;
        level += 1;
        EXPBar.SetLevelText(level);
    }

    // UPGRADE & UNLOCKS =========================================================================================

    // This function chooses which upgrades to present to the player when they level up.
    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();
        
        if (count > availableUpgrades.Count) // if there aren't enough upgrades left, only get what we can
        {
            count = availableUpgrades.Count;
        }

        for(int i = 0; i < count; i++)
        {
            upgradeList.Add(availableUpgrades[Random.Range(0, availableUpgrades.Count)]);
        }

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
                weaponManager.AddWeapon(upgradeData.weaponData);
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

        acquiredUpgrades.Add(upgradeData);
        availableUpgrades.Remove(upgradeData);
    }

    internal void AddToListOfAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        availableUpgrades.AddRange(upgradesToAdd);
    }
}
