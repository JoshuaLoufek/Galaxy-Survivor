 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyHealth : MonoBehaviour, IDamageable, IEnemyHealth
{
    // GLOBAL VARIABLES ==========================================================================================

    [SerializeField] float health = 1f;

    EnemiesManager enemyManager;

    // INITIALIZATION FUNCTIONS ==================================================================================

    public void InitializeEnemyHealth(EnemyData enemyData, EnemiesManager em)
    {
        health = enemyData.stats.health;
        enemyManager = em;
    }

    // UPDATE FUNCTION ===========================================================================================

    private void Update()
    {
        CheckIfDead();
    }

    // HELPER FUNCTIONS ==========================================================================================
    public void TakeDamage(float damage)
    {
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
