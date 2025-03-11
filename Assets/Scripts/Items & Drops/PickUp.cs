using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Rigidbody2D rb;
    bool magnetActivated;
    Vector3 targetPosition;
    public float moveSpeed = 5f;
    Transform targetTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(magnetActivated)
        {
            targetPosition = targetTransform.position;
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
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
