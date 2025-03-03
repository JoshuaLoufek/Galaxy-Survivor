using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePlayer : MonoBehaviour
{
    public PlayerShipSO[] availableShips; // array of all the avilable classes
    PlayerShipSO myShip; // the class the player has chosen

    //public Sprite shipArt;
    public SpriteRenderer shipArt;

    private void Awake()
    {
        Initialize();
        myShip.Print();
    }

    private void Start()
    {
        GetComponent<PlayerController>().SetBaseSpeed(myShip.speed);
        GetComponent<PlayerStats>().SetMaxHealth(myShip.health);
        print("Max Health: " + myShip.health);
    }

    private void Initialize()
    {
        // Important Note: "myShip" is a scriptable object that stores data and functions tied to the chosen ship.
        myShip = availableShips[Random.Range(0, availableShips.Length)]; // This set's the chosen ship. Currently decided at random
        
        shipArt.sprite = myShip.shipArt; // This sets the ship's sprite

        
    }
}
