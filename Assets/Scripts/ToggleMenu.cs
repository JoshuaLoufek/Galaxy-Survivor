using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    bool isActive = false;

    public void Awake()
    {
        isActive = false;
    }

    public void Toggle()
    {
        if (isActive)
        {
            panel.SetActive(false);
            isActive = false;
        }
        else
        {
            panel.SetActive(true);
            isActive = true;
        }
    }
}
