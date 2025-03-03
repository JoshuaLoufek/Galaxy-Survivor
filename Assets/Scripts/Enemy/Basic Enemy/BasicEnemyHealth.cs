 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour, IDamageable, IEnemyHealth
{
    // GLOBAL VARIABLES =============================================================================

    [SerializeField] int health = 1;
    float spawnImmunity = 1f;
    float spawnImmunityTimer;

    EnemiesManager enemyManager;

    // INITIALIZATION FUNCTIONS =====================================================================

    private void Awake()
    {
        spawnImmunityTimer = 0f;
    }

    private void Update()
    {
        spawnImmunityTimer += Time.deltaTime;
    }

    public void InitializeEnemyHealth(EnemiesManager em)
    {
        enemyManager = em;
    }

    // HELPER FUNCTIONS =============================================================================
    public void TakeDamage(int damage)
    {
        // Don't take damage if the enemy still has spawn immunity
        if (spawnImmunityTimer <= spawnImmunity) return;

        health -= damage;

        if (health <= 0) // maybe move this to an update function, but what other function would damage the enemy?
        {
            // signal to the manager this enemy has died
            enemyManager.EnemyDied();

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
}
