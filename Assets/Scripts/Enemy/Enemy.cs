using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Initializer script for all enemies.
    // Intended to:
    // set the attack target,
    // set the movement target
    // help the health component communicate with the enemy manager script

    // Could have this script be a general enemy script and use different initalize enemy functions as needed

    public void InitializeEnemy(Vector2 spawn, GameObject player, EnemiesManager enemyManager)
    {
        // Set spawn location
        this.transform.position = spawn;

        // Initialize scripts on the enemy
        GetComponent<IEnemyOffense>().InitializeEnemyOffense(player); // Initialize the offense script (controls movement + attack)
        GetComponent<IEnemyHealth>().InitializeEnemyHealth(enemyManager); // Link the health script to the enemy manager
    }
}
