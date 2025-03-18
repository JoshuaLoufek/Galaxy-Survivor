using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public GameObject weaponPrefab;
    public WeaponStats stats;
    public List<UpgradeData> upgrades;
}

[Serializable]
public class WeaponStats
{
    public float damage;
    public float pierce;
    public float timeToAttack;
    public float projectileSpeed;
    public float aoe;
    public float extraAttacks;
    public float attackDuration;
    public float critChance;
    public float critDamage;

    // Empty constructor where the stats will be filled in later
    public WeaponStats()
    {

    }

    // Full constructor where the stats are initalized with the WeaponStats object
    public WeaponStats(float damage, float pierce, float timeToAttack, float projectileSpeed, float aoe, float extraAttacks, float attackDuration, float critChance, float critDamage) // Constructor object for weapon stats
    {
        this.damage = damage;
        this.pierce = pierce;
        this.timeToAttack = timeToAttack;
        this.projectileSpeed = projectileSpeed;
        this.aoe = aoe;
        this.extraAttacks = extraAttacks;
        this.attackDuration = attackDuration;
        this.critChance = critChance;
        this.critDamage = critDamage;
    }

    internal void SumStats(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.pierce += weaponUpgradeStats.pierce;
    }

    
}
