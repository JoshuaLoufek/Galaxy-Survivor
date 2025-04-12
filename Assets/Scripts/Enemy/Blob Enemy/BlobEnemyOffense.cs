using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobEnemyOffense : MonoBehaviour, IEnemyOffense
{
    // GLOBAL VARIABLES =============================================================================

    // References to the player the enemy is tracking
    Transform targetDestination; // Set as the player's current location.
    GameObject targetGameObject; // A reference to the player game object that's created on awake.
    PlayerHealth playerHealth; // A reference to the player health script.

    // References to this enemy
    Rigidbody2D enemyRB;
    [SerializeField] float moveForce = 10f;
    [SerializeField] float moveInterval = 3f;
    float moveTimer;

    [SerializeField] int contactDamage = 1;
    float contactDamageRate = 1f;
    float contactDamageTimer = 0f;

    // INITIALIZATION FUNCTIONS =====================================================================

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        moveTimer = 0f;
        contactDamageTimer = 0f;
    }

    public void InitializeEnemyOffense(EnemyData enemyData, GameObject target)
    {
        contactDamage = enemyData.stats.damage;
        moveForce = 500f * enemyData.stats.moveSpeed;

        targetGameObject = target;
        targetDestination = target.transform;
        playerHealth = target.GetComponent<PlayerHealth>();
    }

    // UPDATE FUNCTIONS =============================================================================

    void FixedUpdate()
    {
        contactDamageTimer += Time.deltaTime;

        moveTimer += Time.deltaTime;
        if(moveTimer >= moveInterval)
        {
            MoveEnemy();
            moveTimer = 0f;
        }
    }

    // HELPER FUNCTIONS =============================================================================

    private void MoveEnemy()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        enemyRB.AddForce(direction * moveForce);
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
        if (contactDamageTimer > contactDamageRate)
        {
            playerHealth.TakeDamage(contactDamage);
            contactDamageTimer = 0f;
        }
    }
}
