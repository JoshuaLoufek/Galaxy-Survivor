using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : MonoBehaviour, IPickUpObject
{
    [SerializeField] int heal = 5;

    public void OnPickUp(PlayerHealth player)
    {
        player.Heal(heal);
    }
}
