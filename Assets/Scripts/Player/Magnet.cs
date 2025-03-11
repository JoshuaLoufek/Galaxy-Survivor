using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        // If the magnet collider hits an object with the pickup script...
        if(collision.gameObject.TryGetComponent<PickUp>(out PickUp pickUp))
        {
            // Tell the object to come to the player.
            pickUp.SetTarget(this.transform.parent);
        }
    }
}
