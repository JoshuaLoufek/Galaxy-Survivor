using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Ship", menuName = "Player Ship")]
public class PlayerShipSO : ScriptableObject
{
    public string shipName; // the name of this ship class
    public Sprite shipArt; // represents the body of the ship

    public int health;
    public float speed;

    public void Print()
    {
        Debug.Log(shipName + ": This is a ship");
    }
}
