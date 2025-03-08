using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    // This method is invoked by pauseGameEvent located in the PlayerController script
    public void TogglePauseMenu()
    {
        if (pausePanel.activeInHierarchy) 
        {
            CloseMenu(); 
        }
        else 
        {
            OpenMenu(); 
        }
    }

    public void OpenMenu()
    {
        pauseManager.PauseGame();
        pausePanel.SetActive(true);
    }

    // This method is also invoked by the "Exit" button of the pause menu.
    public void CloseMenu()
    {
        pauseManager.UnpauseGame();
        pausePanel.SetActive(false);
    }
}
