using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }
}
