using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject pickup; // will eventually want this to be an array of possible drops
    [SerializeField] List<GameObject> itemDropList;

    // Alongside an array of drop chances for each possible item.
    // These combined will allow any entity that can drop an item (static objects, enemies, etc.) to use this script
    // set up exceptions to be thrown in case 1. the possible drops and drop chance arrays aren't equal and 2. the drop chances don't add up to 100

    public void DropItem()
    {
        if (pickup != null)
        {
            Transform t = Instantiate(pickup).transform;
            t.position = transform.position;
        } 
        else
        {
            Debug.Log("Missing pickup prefab to drop!");
        }
        
    }

    internal void SetExpDrop(PickupExperience expDrop)
    {
        pickup = expDrop.gameObject;
    }
}
