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
}

[Serializable]
public class WeaponStats
{
    public int damage;
    public float timeToAttack;

    public WeaponStats(int damage, float timeToAttack) // Constructor object for weapon stats
    {
        this.damage = damage;
        this.timeToAttack = timeToAttack;
    }
}
