using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUnlock, // definitely staying
    WeaponUpgrade, // definitely staying
    ItemUnlock, // do I want to use items to upgrade player stats?
    ItemUpgrade,
    RelicUnlock, // activated item with a cooldown
    RelicUpgrade
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    // Universal Upgrade Variables
    public UpgradeType upgradeType; // How the player Level script should interpret this upgrade
    public string Name; // The name of the upgrade
    public string description; // A description of the upgrade
    public int level; // The level the player needs to be for this upgrade to be avaliable to them
    public Sprite icon; // How the upgrade is visually represented to the player
    
    // Weapon Specific
    public WeaponData weaponData; // Holds a reference to the weapon that this upgrade belongs to.
    public WeaponStats weaponUpgradeStats; // Holds the actual upgrade stats

    // Also Universal, but deals specifically with what happens after this upgrade is chosen
    public List<UpgradeData> addWhenChosen; // Upgrade(s) that are added to the list when this upgrade is chosen.
    public List<UpgradeData> removeWhenChosen; // Upgrade(s) that should be removed from the list when this upgrade is chosen.
}
