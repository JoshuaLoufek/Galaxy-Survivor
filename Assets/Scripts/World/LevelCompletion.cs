using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteLevel;

    StageTime stageTime;
    PauseManager pauseManager;
    VictoryPanel victoryPanel;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Start()
    {
        pauseManager = FindFirstObjectByType<PauseManager>();
        victoryPanel = FindFirstObjectByType<VictoryPanel>(FindObjectsInactive.Include);
    }

    private void Update()
    {
        if (stageTime.time > timeToCompleteLevel)
        {
            pauseManager.PauseGame();
            victoryPanel.gameObject.SetActive(true);
        }
    }
}
