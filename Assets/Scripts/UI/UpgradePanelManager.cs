using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }


    public void OpenPanel()
    {
        pauseManager.PauseGame();
        upgradePanel.SetActive(true);
    }

    public void ClosePanel()
    {
        pauseManager.UnpauseGame();
        upgradePanel.SetActive(false);
    }
}
