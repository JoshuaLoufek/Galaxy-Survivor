using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyOffense : MonoBehaviour, IEnemyOffense
{
    // GLOBAL VARIABLES =============================================================================

    // References to the player the enemy is tracking
    Transform targetDestination; // Set as the player's current location.
    GameObject targetGameObject; // A reference to the player game object that's created on awake.
    PlayerStats playerHealth; // A reference to the player health script.

    // References to this enemy
    Rigidbody2D enemyRB;
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] int contactDamage = 1;
    float contactDamageRate = 1f;
    float timer = 0f;

    // INITIALIZATION FUNCTIONS =====================================================================

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        timer = 0f;
    }

    public void InitializeEnemyOffense(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
        playerHealth = target.GetComponent<PlayerStats>();
    }

    // UPDATE FUNCTIONS =============================================================================

    void FixedUpdate()
    {
        MoveEnemy();
        if (timer >= 0f) // Timer until the enemy can damage the player again
        {
            timer -= Time.deltaTime;
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
            Attack();
        }
    }

    private void Attack()
    {
        if (timer <= 0f)
        {
            playerHealth.DamagePlayer(contactDamage);
            timer = contactDamageRate;
        }
    }
}
