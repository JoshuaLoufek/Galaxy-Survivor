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
    public int damage;
    public int pierce;
    public float timeToAttack;
    public float projectileSpeed;
    public float aoe;
    public float extraAttacks;
    public float attackDuration;
    public float critChance;
    public float critDamage;

    public WeaponStats(int damage, float timeToAttack) // Constructor object for weapon stats
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
    }

    internal void SumStats(WeaponStats weaponUpgradeStats)
    {
        this.damage += weaponUpgradeStats.damage;
        this.timeToAttack += weaponUpgradeStats.timeToAttack;
        this.pierce += weaponUpgradeStats.pierce;
    }
}
