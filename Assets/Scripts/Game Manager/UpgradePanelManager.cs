using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject upgradePanel;
    PauseManager pauseManager;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    private void Start()
    {
        DisableButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        CleanPanel();
        pauseManager.PauseGame();
        upgradePanel.SetActive(true);

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }

        upgradeButtons[0].gameObject.GetComponent<Button>().Select(); // sets the first button to be active by default
    }

    public void ClosePanel()
    {
        DisableButtons();

        pauseManager.UnpauseGame();
        upgradePanel.SetActive(false);
    }

    private void DisableButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }

    // This is called from the Upgrade Button OnClick event
    public void Upgrade(int pressedButtonID)
    {
        GameManager.instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);
        ClosePanel();
    }

    public void CleanPanel() // cleans the sprites currently associated with button (set to null)
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }
}
