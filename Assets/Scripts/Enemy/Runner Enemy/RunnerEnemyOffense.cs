using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerEnemyOffense : MonoBehaviour, IEnemyOffense
{
    // GLOBAL VARIABLES =============================================================================

    // References to the player the enemy is tracking
    Transform targetDestination; // Set as the player's current location.
    GameObject targetGameObject; // A reference to the player game object that's created on awake.
    PlayerStats playerHealth; // A reference to the player health script.

    // References to this enemy
    Rigidbody2D enemyRB;
    [SerializeField] float baseMoveSpeed = 10f;
    [SerializeField] float accelerationInterval = 1.5f;
    float accelerationFactor;
    float accelerationTimer;

    [SerializeField] int contactDamage = 1;
    float contactDamageRate = 1f;
    float contactDamageTimer;

    // INITIALIZATION FUNCTIONS =====================================================================

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        accelerationTimer = 0f;
        contactDamageTimer = 0;
        accelerationFactor = 1f;
    }

    public void InitializeEnemyOffense(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
        playerHealth = target.GetComponent<PlayerStats>();
    }

    // UPDATE FUNCTIONS =============================================================================

    void Update()
    {
        MoveEnemy();
        
        accelerationTimer += Time.deltaTime;
        contactDamageTimer += Time.deltaTime;

        if (accelerationTimer >= accelerationInterval)
        {
            accelerationTimer = 0f;
            accelerationFactor += 1f;
        }
    }

    // HELPER FUNCTIONS =============================================================================

    private void MoveEnemy()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        enemyRB.AddForce(direction * baseMoveSpeed * accelerationFactor);
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
        if (contactDamageTimer >= contactDamageRate)
        {
            playerHealth.DamagePlayer(contactDamage);
            contactDamageTimer = contactDamageRate;
        }
    }
}
