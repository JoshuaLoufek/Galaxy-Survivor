using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody2D rb;
    Transform targetTransform;
    bool magnetActivated;
    public float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(magnetActivated)
        {
            Vector2 targetDirection = (targetTransform.position - transform.position).normalized;
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * moveSpeed;
        }
    }

    // When the pickup object collides with the player, the effect happens and then it's destroyed.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth p = collision.GetComponent<PlayerHealth>();
        if (p != null)
        {
            GetComponent<IPickUpObject>().OnPickUp(p);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        targetTransform = target;
        magnetActivated = true;
    }
}
