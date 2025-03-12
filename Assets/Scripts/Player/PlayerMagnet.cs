using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    PlayerStats playerStats;
    CircleCollider2D circleCollider;
    private float baseRadius = 3f;

    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        circleCollider = GetComponent<CircleCollider2D>();
        CalculateMagnetRadius();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the magnet collider hits an object with the pickup script...
        if(collision.gameObject.TryGetComponent<PickUp>(out PickUp pickUp))
        {
            // Tell the object to come to the player.
            pickUp.SetTarget(this.transform.parent);
        }
    }

    public void CalculateMagnetRadius()
    {
        circleCollider.radius = baseRadius * (1 + playerStats.pickupRange);
    }
}
