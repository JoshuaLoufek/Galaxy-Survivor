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

    EnemyData enemyData;

    public void InitializeEnemy(EnemyData data, Vector2 spawn, GameObject player, EnemiesManager enemyManager)
    {
        // Grab the data necessary to initalize the enemy's stats
        enemyData = data;

        // Set spawn location
        this.transform.position = spawn;

        // Initialize scripts on the enemy
        GetComponent<IEnemyOffense>().InitializeEnemyOffense(enemyData, player); // Initialize the offense script (controls movement + attack)
        GetComponent<IEnemyHealth>().InitializeEnemyHealth(enemyData, enemyManager); // Link the health script to the enemy manager
        GetComponent<DropOnDestroy>().SetExpDrop(enemyData.expDrop);
    }
}
