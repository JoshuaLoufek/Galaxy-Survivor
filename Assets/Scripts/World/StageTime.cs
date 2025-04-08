using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    public float time;
    TimerUI timerUI;

    private void Start()
    {
        timerUI = FindFirstObjectByType<TimerUI>(); // when placed in awake it was too fast to grab the other component
    }

    private void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);
    }
}
