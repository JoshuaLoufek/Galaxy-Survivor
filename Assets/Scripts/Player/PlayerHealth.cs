using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    // Player Scripts (and other objects)
    [HideInInspector] public Level level; // (dynamic) set on awake
    [HideInInspector] public Money money; // (dyanmic) set on awake
    [HideInInspector] public PlayerStats playerStats; // (dynamic) set on awake
    [SerializeField] StatusBar healthBar; // (static) set in inspector

    // Base Stats (intended for stats that need a base value to be initialized)
    private int baseMaxHealth;
    private float baseHealthRegen;

    // Max Stats (intended for stats that will have a current value that changes dynamically with gameplay) 
    public int maxHealth;

    // Current Stats (the stat that's "used" in practice)
    public int currentHealth;
    public float healthRegen; // hp regenerated per second
    public float defense;

    // Player States and other variables
    public bool isDead;
    private float regenPool;

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        level = GetComponent<Level>();
        money = GetComponent<Money>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        isDead = false;
        regenPool = 0f;
        InitializePlayerHealth();
    }

    private void InitializePlayerHealth()
    {
        baseMaxHealth = 100;
        baseHealthRegen = 0.2f;

        CalculateMaxHealth();
        CalculateHealthRegen();
        CalculateDefense();

        currentHealth = maxHealth;
    }

    // UPDATE FUNCTION ===========================================================================================

    private void Update()
    {
        RegenHealth();
    }

    // HELPER FUNCTIONS ==========================================================================================

    private void RegenHealth()
    {
        // The regen pool fills up to 1, and then overflows and heals the player
        regenPool += healthRegen * Time.deltaTime;
        if (regenPool >= 1f)
        {
            regenPool -= 1f;
            Heal(1);
        }
    }

    public void TakeDamage(int damage) // Takes a positive damage number
    {
        if (isDead) return;
        
        ApplyDefense(ref damage);
        
        currentHealth -= damage; // Damages the player
        healthBar.SetState(currentHealth, maxHealth); // Updates the player's hp bar
        if (currentHealth <= 0) // GAME OVER state
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }
    }

    private void ApplyDefense(ref int damage)
    {
        damage -= (int)defense;
        if (damage <= 1) { damage = 1; } // damage can't be reduced below 1
    }

    public void Heal(int heal)
    { 
        if (currentHealth <= 0) { return; } // Early exit if the player is dead

        currentHealth += heal; // Heals the player
        if (currentHealth >= maxHealth) { currentHealth = maxHealth; } // Ensures Health doesn't go above max
        healthBar.SetState(currentHealth, maxHealth); // Updates the player's hp bar
    }

    // STATISTIC CALCULATOR FUNCTIONS =====================================================================================
        // This is where the final, usable versions of each stat are calculated.
        // They will be applied in the functions that are above.

    public void CalculateMaxHealth()
    {
        maxHealth = (int)(baseMaxHealth * (1 + playerStats.maxHealth));
        // Debug.Log("Max Health Set: " + maxHealth);
    }

    public void CalculateHealthRegen()
    {
        healthRegen = (baseHealthRegen * (1 + playerStats.healthRegen));
        // Debug.Log("Health Regen Set: " + healthRegen);
    }

    public void CalculateDefense()
    {
        defense = (playerStats.defense);
        // Debug.Log("Defense Set: " + defense);
    }
}
