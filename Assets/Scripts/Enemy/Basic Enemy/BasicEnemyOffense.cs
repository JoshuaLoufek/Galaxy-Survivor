using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyOffense : MonoBehaviour, IEnemyOffense
{
    // GLOBAL VARIABLES =============================================================================

    // References to the player the enemy is tracking
    Transform targetDestination; // Set as the player's current location.
    GameObject targetGameObject; // A reference to the player game object that's created on awake.
    PlayerHealth playerHealth; // A reference to the player health script.

    // References to this enemy
    Rigidbody2D enemyRB;
    [SerializeField] float moveSpeed;
    [SerializeField] int contactDamage = 1;
    float contactDamageRate = 1f;
    float ContactDamageTimer = 0f;

    // INITIALIZATION FUNCTIONS =====================================================================

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        ContactDamageTimer = 0f;
    }

    public void InitializeEnemyOffense(EnemyData enemyData, GameObject target)
    {
        contactDamage = enemyData.stats.damage;
        moveSpeed = enemyData.stats.moveSpeed;

        targetGameObject = target;
        targetDestination = target.transform;
        playerHealth = target.GetComponent<PlayerHealth>();
    }

    // UPDATE FUNCTIONS =============================================================================

    void FixedUpdate()
    {
        MoveEnemy();
        if (ContactDamageTimer >= 0f) // Timer until the enemy can damage the player again
        {
            ContactDamageTimer -= Time.deltaTime;
        }
    }

    // HELPER FUNCTIONS =============================================================================

    private void MoveEnemy()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        enemyRB.velocity = direction * moveSpeed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // checks if the object colliding with the enemy is the player
        if (collision.gameObject == targetGameObject)
        {
            ContactAttack();
        }
    }

    private void ContactAttack()
    {
        if (ContactDamageTimer <= 0f)
        {
            playerHealth.TakeDamage(contactDamage);
            ContactDamageTimer = contactDamageRate;
        }
    }
}
