using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupExperience : MonoBehaviour, IPickUpObject
{
    [SerializeField] int exp = 200;

    public void OnPickUp(PlayerHealth player)
    {
        player.level.AddExperience(exp);
    }
}
