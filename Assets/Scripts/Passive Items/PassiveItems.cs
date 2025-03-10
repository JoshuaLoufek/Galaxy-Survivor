using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<Item> equippedItems;

    PlayerHealth playerStats;
    PlayerController playerController;

    private void Awake()
    {
        playerStats = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
    }

    public void Equip(Item itemToEquip)
    {
        if (equippedItems == null) { equippedItems = new List<Item>(); } // Creates a new list if needed

        equippedItems.Add(itemToEquip); // Adds the item to the list of equiped items
        itemToEquip.Equip(playerStats, playerController); // Calls the equip method of the item which will modify the appropriate stats

    }

    public void Unequip(Item itemToUnequip)
    {

    }
}
