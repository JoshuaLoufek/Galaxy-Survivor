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
    public UpgradeType upgradeType;
    public string Name;
    public Sprite icon;

    public WeaponData weaponData;
}
