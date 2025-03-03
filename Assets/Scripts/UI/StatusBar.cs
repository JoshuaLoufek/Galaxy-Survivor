using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] Transform bar; // References the red bar container below this script in the heirarchy
    [SerializeField] TMPro.TextMeshProUGUI levelText;
    [SerializeField] TMPro.TextMeshProUGUI moneyText;

    public void SetState(int current, int max)
    {
        float state = (float)current;
        state /= max;
        if (state < 0f) { state = 0f; }
        bar.transform.localScale = new Vector3(state, 1f, 1f);
    }

    public void SetLevelText(int level)
    {
        if(levelText != null)
        {
            levelText.text = "LEVEL: " + level.ToString();
        }
    }
}
