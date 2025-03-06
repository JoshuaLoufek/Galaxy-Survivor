using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    int level = 1;
    int experience = 0;

    [SerializeField] StatusBar EXPBar;
    [SerializeField] UpgradePanelManager upgradePanelManager;

    int TO_LEVEL_UP
    {
        get
        {
            return level * 1000;
        }
    }

    private void Start()
    {
        EXPBar.SetState(experience, TO_LEVEL_UP);
        EXPBar.SetLevelText(level);
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        EXPBar.SetState(experience, TO_LEVEL_UP);
    }

    public void CheckLevelUp()
    {
        if (experience >= TO_LEVEL_UP)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        upgradePanelManager.OpenPanel();
        experience -= TO_LEVEL_UP;
        level += 1;
        EXPBar.SetLevelText(level);
    }
}
