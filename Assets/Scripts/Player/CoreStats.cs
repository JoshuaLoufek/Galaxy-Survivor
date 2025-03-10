using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CoreStats : MonoBehaviour
{
    // The "core" stats
    public int power;
    public int speed;
    public int durability;
    public int intelligence;
    public int amplify;
    public int capacity;
    public int luck;

    // Basic Constructor
    public CoreStats(int power, int speed, int durability, int intelligence, int amplify, int capacity, int luck)
    {
        this.power = power;
        this.speed = speed;
        this.durability = durability;
        this.intelligence = intelligence;
        this.amplify = amplify;
        this.capacity = capacity;
        this.luck = luck;
    }

    // Functions

}
