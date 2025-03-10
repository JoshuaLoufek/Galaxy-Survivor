using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CoreStats
{
    // The Core Stats
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

    // Setter Functions
    public void SetPower(int power) { this.power = power; }

    public void SetSpeed(int speed) { this.speed = speed; }

    public void SetDurability(int durability) { this.durability = durability; }

    public void SetIntelligence(int intelligence) { this.intelligence = intelligence; }

    public void SetAmplify(int amplify) { this.amplify = amplify; }

    public void SetCapacity(int capacity) { this.capacity = capacity; }
    
    public void SetLuck(int luck) { this.luck = luck; }
}
