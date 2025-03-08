using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject playerWeapons;

    public void GameOver()
    {
        Debug.Log("Game Over");

        GetComponent<PlayerController>().enabled = false;
        
        playerWeapons.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
