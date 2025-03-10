using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth p = collision.GetComponent<PlayerHealth>();
        if (p != null)
        {
            GetComponent<IPickUpObject>().OnPickUp(p);
            Destroy(gameObject);
        }
    }
}
