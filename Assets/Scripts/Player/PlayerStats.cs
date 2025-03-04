using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    public int maxHealth = 10;
    public int currentHealth = 10;
    public int armor = 0;

    [SerializeField] StatusBar healthBar; // (static) set in inspector
    [HideInInspector] public Level level; // (dynamic) set on awake
    [HideInInspector] public Money money; // (dyanmic) set on awake

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        level = GetComponent<Level>();
        money = GetComponent<Money>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // HEALTH FUNCTIONS ==========================================================================================

    public void TakeDamage(int damage) // Takes a positive damage number
    {
        ApplyArmor(ref damage);
        
        currentHealth -= damage; // Damages the player
        healthBar.SetState(currentHealth, maxHealth); // Updates the player's hp bar
        if (currentHealth <= 0) // GAME OVER state
        {
            print("Player has died! GAME OVER.");
        }
    }

    public void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if (damage <= 0) { damage = 0; }
    }

    public void Heal(int heal)
    { 
        if (currentHealth <= 0) { return; } // Early exit if the player is dead

        currentHealth += heal; // Heals the player
        if (currentHealth >= maxHealth) { currentHealth = maxHealth; } // Ensures Health doesn't go above max
        healthBar.SetState(currentHealth, maxHealth); // Updates the player's hp bar
    }

    public void SetMaxHealth(int myMaxHealth)
    {
        print("Setting max health");
        maxHealth = myMaxHealth;
        currentHealth = myMaxHealth;
    }
}
