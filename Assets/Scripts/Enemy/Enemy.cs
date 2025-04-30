using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    GameObject player;
    EnemiesManager enemyManager;

    bool isInitialized;

    private void Awake()
    {
        isInitialized = false;
    }

    public void InitializeEnemy(EnemyData data, Vector2 spawn, GameObject thePlayer, EnemiesManager em)
    {
        // Grab the data necessary to initalize the enemy's stats
        enemyData = data;
        player = thePlayer;
        enemyManager = em;

        // Set spawn location
        this.transform.position = spawn;

        // Initialize scripts on the enemy
        GetComponent<IEnemyOffense>().InitializeEnemyOffense(enemyData, player); // Initialize the offense script (controls movement + attack)
        GetComponent<IEnemyHealth>().InitializeEnemyHealth(enemyData, enemyManager); // Link the health script to the enemy manager
        GetComponent<DropOnDestroy>().SetExpDrop(enemyData.expDrop);

        isInitialized = true;
    }

    private void Update()
    {
        if (isInitialized)
        {
            CheckDistanceFromPlayer();
        }
    }

    // This function is designed to move the enemy to the player if the player gets too far away
    private void CheckDistanceFromPlayer()
    {
        float x = MathF.Abs(this.transform.position.x - player.transform.position.x);
        float y = MathF.Abs(this.transform.position.y - player.transform.position.y);
        // float distanceFromPlayer = Mathf.Sqrt(MathF.Pow(x, 2) + MathF.Pow(y, 2));

        // The enemy spawn rectangle around the player is a 30x20. If the enemy goes farther than 40 from the player they should be brought back 
        if (x > (enemyManager.spawnRect.x + 5f) || y > (enemyManager.spawnRect.y + 5f))
        {
            this.transform.position = enemyManager.GenerateRandomPosition();
        }
    }
}
