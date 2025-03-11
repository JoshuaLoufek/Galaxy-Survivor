using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMagnet : MonoBehaviour, IPickUpObject
{
    public void OnPickUp(PlayerHealth player)
    {
        // Whenever the magnet is picked up, find all relevant objects...
        var expDrops = FindObjectsByType<PickupExperience>(FindObjectsSortMode.None);
        // And activate their magnet tracking.
        foreach (var exp in expDrops)
        {
            exp.GetComponent<PickUp>().SetTarget(player.transform);
        }
    }
}
