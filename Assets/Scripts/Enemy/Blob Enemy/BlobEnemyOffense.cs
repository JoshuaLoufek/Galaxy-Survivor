using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobEnemyOffense : MonoBehaviour, IEnemyOffense
{
    // GLOBAL VARIABLES =============================================================================

    // References to the player the enemy is tracking
    Transform targetDestination; // Set as the player's current location.
    GameObject targetGameObject; // A reference to the player game object that's created on awake.
    PlayerStats playerHealth; // A reference to the player health script.

    // References to this enemy
    Rigidbody2D enemyRB;
    [SerializeField] float moveForce = 10f;
    [SerializeField] float moveInterval = 3f;
    float moveTimer;

    [SerializeField] int contactDamage = 1;
    float contactDamageRate = 1f;
    float timer = 0f;

    // INITIALIZATION FUNCTIONS =====================================================================

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        moveTimer = 0f;
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
}
