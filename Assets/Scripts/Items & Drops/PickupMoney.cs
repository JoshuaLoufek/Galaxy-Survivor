using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMoney : MonoBehaviour, IPickUpObject
{
    [SerializeField] int value = 200;

    public void OnPickUp(PlayerHealth player)
    {
        player.money.AddMoney(value);
    }
}
