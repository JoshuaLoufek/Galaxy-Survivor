 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour, IDamageable, IEnemyHealth
{
    // GLOBAL VARIABLES =============================================================================

    [SerializeField] float health = 1f;
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
        CheckIfDead();
    }

    public void InitializeEnemyHealth(EnemiesManager em)
    {
        enemyManager = em;
    }

    // HELPER FUNCTIONS =============================================================================
    public void TakeDamage(float damage)
    {
        // Don't take damage if the enemy still has spawn immunity
        if (spawnImmunityTimer <= spawnImmunity) return;

        health -= damage;
    }

    private void CheckIfDead()
    {
        if (health <= 0)
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
