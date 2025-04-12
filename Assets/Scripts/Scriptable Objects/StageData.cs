using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageEvent
{
    public float time; // When this wave of enemies will be spawned in.
    public string message;
    public EnemyData enemyData; // The data container that describes the Enemy and references the prefab
    public int enemyCount; // How many enemies should be spawned
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents; // A list of stage events
}
