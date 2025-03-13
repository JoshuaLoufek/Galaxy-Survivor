using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        // Call the item drop script
        DropOnDestroy drop = gameObject.GetComponent<DropOnDestroy>();
        if (drop != null)
        {
            drop.DropItem();
        }

        // Destroy this game object
        Destroy(gameObject);
    }
}
