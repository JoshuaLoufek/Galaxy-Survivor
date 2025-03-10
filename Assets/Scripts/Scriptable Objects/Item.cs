using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStats // any stats that an item can modify will go here. Any stats the item doesn't want to use will be left as zero.
{
    public int armor;
    public int moveSpeed;
    public int damageBonus;
}

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public ItemStats stats;

    public void Equip(PlayerHealth playerStats, PlayerController playerController)
    {
        playerStats.defense += stats.armor;
        playerController.moveSpeed += stats.moveSpeed;
    }

    public void Unequip(PlayerHealth playerStats, PlayerController playerController)
    {
        playerStats.defense -= stats.armor;
        playerController.moveSpeed -= stats.moveSpeed;
    }
}
