using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnRect;
    [SerializeField] float spawnTimer;
    GameObject player;
    float timer;
    [SerializeField] int maxSpawns = 10;
    int currentSpawns = 0;
    int totalKills;

    private void Awake()
    {
        currentSpawns = 0;
        totalKills = 0;
    }

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;
    }

    private void FixedUpdate()
    {
        if(timer > 0f) { timer -= Time.deltaTime; }
        
        if(timer <= 0f && currentSpawns < maxSpawns)
        {
            timer = spawnTimer;
            currentSpawns++;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float x = 0;
        float y = 0;

        // Determines the spawning coordinates of the enemy and ensures it's outside of the player's view on the x or y axis.
        if ((Random.Range(0, 2) == 0)) { // This randomly chooses the x or the y to be outside the player's vision
            x = spawnRect.x * (Random.Range(0, 2) * 2 - 1); // Locked outside player vision
            y = UnityEngine.Random.Range(-spawnRect.y, spawnRect.y); // Can dynamically place on this axis
        } else {
            x = UnityEngine.Random.Range(-spawnRect.x, spawnRect.x); // Can dynamically place on this axis
            y = spawnRect.y * (Random.Range(0, 2) * 2 - 1); // Locked outside player vision
        }   

        // Sets up the vector for enemies to spawn on
        Vector3 position = new Vector3(x, y, 0f);
        
        // Sets the enemy's position to be relative to the player
        position += player.transform.position; 

        // Create the enemy and set it to follow and attack the player
        GameObject newEnemy = Instantiate(enemy); // Create the enemy
        newEnemy.GetComponent<Enemy>().InitializeEnemy(position, player, this); // Initialize the enemy scripts
        newEnemy.transform.parent = transform; // Set's the enemy to be a child of this object. Keeps the scene hierarchy clean.
    }

    public void EnemyDied()
    {
        totalKills += 1;
        Debug.Log("Enemy Died. Total Kills: " + totalKills);
        currentSpawns--;
    }
}
