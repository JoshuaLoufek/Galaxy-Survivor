using System;
using UnityEngine;

// This script represents the buffs the player recieves from leveling up their core stats
// In other words, this script is a giant data container filled with all the player's innate stat buffs
public class PlayerStats : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    // Player Scripts
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private PlayerWeapons playerWeapons;
    private Level level;

    // CORE STATS
    public CoreStats coreStats;

    // POWER
    public float damage;
    public float pierce;
    // SPEED
    public float moveSpeed;
    public float attackSpeed;
    public float projectileSpeed;
    // DURABILITY
    public float maxHealth;
    public float healthRegen;
    public float defense;
    // INTELLIGENCE
    public float bonusEXP;
    public float pickupRange;
    // AMPLIFY
    public float aoe;
    public float extraAttacks;
    // CAPACITY
    public float attackDuration;
    // LUCK
    public float critChance;
    public float critDamage;

    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();
        playerWeapons = GetComponent<PlayerWeapons>();
        level = GetComponent<Level>();

        InitializeCoreStats();
        UpdateAllStats();
    }

    private void InitializeCoreStats()
    {
        coreStats = new CoreStats(0, 0, 0, 0, 0, 0, 0);
    }

    // BUFF CORE STAT FUNCTIONS ================================================================================

    public void BuffPower(int buff)
    {
        coreStats.SetPower(coreStats.power + buff);
        UpdatePowerStats();
    }

    public void BuffSpeed(int buff)
    {
        coreStats.SetSpeed(coreStats.speed + buff);
        UpdateSpeedStats();
    }

    public void BuffDurability(int buff)
    {
        coreStats.SetDurability(coreStats.durability + buff);
        UpdateDurabilityStats();
    }

    public void BuffIntelligence(int buff)
    {
        coreStats.SetIntelligence(coreStats.intelligence + buff);
        UpdateIntelligenceStats();
    }

    public void BuffAmplify(int buff)
    {
        coreStats.SetAmplify(coreStats.amplify + buff);
        UpdateAmplifyStats();
    }

    public void BuffCapacity(int buff)
    {
        coreStats.SetCapacity(coreStats.capacity + buff);
        UpdateCapacityStats();
    }

    public void BuffLuck(int buff)
    {
        coreStats.SetLuck(coreStats.luck + buff);
        UpdateLuckStats();
    }

    // UPDATE PLAYER STATS FUNCTIONS =============================================================================

    public void UpdateAllStats()
    {
        UpdatePowerStats();
        UpdateSpeedStats();
        UpdateDurabilityStats();
        UpdateIntelligenceStats();
        UpdateAmplifyStats();
        UpdateCapacityStats();
        UpdateLuckStats();
    }

    // POWER ------------------------------------------------------------------
    public void UpdatePowerStats()
    {
        SetDamage(coreStats.power);
        SetPierce(coreStats.power);
    }

    private void SetDamage(int power)
    {
        // 10% damage bonus per level
        damage = 0.1f * power;
    }

    private void SetPierce(int power)
    {
        // 50% pierce bonus per level
        pierce = 0.5f * power;
    }

    // SPEED ------------------------------------------------------------------
    public void UpdateSpeedStats()
    {
        SetMoveSpeed(coreStats.speed);
        SetAttackSpeed(coreStats.speed);
        SetProjectileSpeed(coreStats.speed);
    }

    private void SetMoveSpeed(float speed)
    {
        // 10% bonus move speed per level
        moveSpeed = 0.1f * speed;
    }

    private void SetAttackSpeed(int speed)
    {
        // 10% bonus attack speed per level
        attackSpeed = 0.1f * speed;
    }

    private void SetProjectileSpeed(int speed)
    {
        // 10% bonus projectile speed per level
        projectileSpeed = 0.1f * speed;
    }

    // DURABILITY -------------------------------------------------------------
    public void UpdateDurabilityStats()
    {
        SetMaxHealth(coreStats.durability);
        SetHealthRegen(coreStats.durability);
        SetDefense(coreStats.durability);
    }

    private void SetMaxHealth(int durability)
    {
        // 10% bonus max health per level
        maxHealth = 0.1f * durability;
        playerHealth.UpdateMaxHealth();
    }

    private void SetHealthRegen(int durability)
    {
        // 25% bonus HP regenerated per level
        healthRegen = 0.25f * durability;
        playerHealth.UpdateHealthRegen();
    }

    private void SetDefense(int durability)
    {
        // 2 defense (damage blocked) per level
        defense = 2f * durability;
        playerHealth.UpdateDefense();
    }

    // INTELLIGENCE -----------------------------------------------------------
    public void UpdateIntelligenceStats()
    {
        SetBonusEXP(coreStats.intelligence);
        SetPickupRange(coreStats.intelligence);
    }

    private void SetBonusEXP(int intelligence)
    {
        // 5% bonus EXP per level
        bonusEXP = 0.05f * intelligence;
    }

    private void SetPickupRange(int intelligence)
    {
        // 20% bonus pickup range per level
        pickupRange = 0.2f * intelligence;
    }

    // AMPLIFY ----------------------------------------------------------------
    private void UpdateAmplifyStats()
    {
        SetAOE(coreStats.amplify);
        SetExtraAttacks(coreStats.amplify);
    }
    private void SetAOE(int amplify)
    {
        // 20% bonus aoe per level
        aoe = 0.2f * amplify;
    }

    private void SetExtraAttacks(int amplify)
    {
        // 10% bonus attacks per level
        extraAttacks = 0.1f * amplify;
    }

    // CAPACITY ---------------------------------------------------------------
    private void UpdateCapacityStats()
    {
        SetAttackDuration(coreStats.capacity);
    }

    private void SetAttackDuration(int capacity)
    {
        // 10% extra attack duration per level
        attackDuration = 0.1f * capacity;
    }

    // LUCK -------------------------------------------------------------------
    private void UpdateLuckStats() // Updates all the combat stats in this script using the current player core stats
    {
        SetCritChance(coreStats.luck);
        SetCritDamage(coreStats.luck);
    }

    private void SetCritChance(int luck)
    {
        // A flat bonus of 3% extra crit chance per level
        // (currently intended to be an additive bonus instead of a multiplier)
        critChance = 0.03f * luck;
    }

    private void SetCritDamage(int luck)
    {
        // 5% extra crit damage per level (how and when to apply the damage?)
        critDamage = 0.5f * luck;
    }
}
