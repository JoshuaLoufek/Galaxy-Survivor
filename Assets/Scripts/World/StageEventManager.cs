using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemiesManager enemiesManager;

    StageTime stageTime;
    int eventIndexer;

    private void Start()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Update()
    {
        // Exit block when there are no more events
        if (eventIndexer >= stageData.stageEvents.Count) { return; }

        // Trigger an (enemy spawn) event at the appropraite time
        if (stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            Debug.Log(stageData.stageEvents[eventIndexer].message);

            for (int i = 0; i < stageData.stageEvents[eventIndexer].enemyCount; i++)
            {
                enemiesManager.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn);
            }

            eventIndexer++;
        }
    }
}
