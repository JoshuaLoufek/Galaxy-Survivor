using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    bool isPaused = false;

    public void Awake()
    {
        isPaused = false;
    }

    public void OnPause(InputValue escValue)
    {
        Debug.Log("pause button clicked");
        if(isPaused)
        {
            pausePanel.SetActive(false);
            isPaused = false;
        }
        else
        {
            pausePanel.SetActive(true);
            isPaused = true;
        }
    }
}
