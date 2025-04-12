using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    public int health;
    public int damage;
    public int moveSpeed;
}

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public string enemyName; // The name of the enemy
    public GameObject enemyPrefab; // The enemy prefab
    public EnemyStats stats; // The enemy stats
    public PickupExperience expDrop; // What type of exp they should drop
}
