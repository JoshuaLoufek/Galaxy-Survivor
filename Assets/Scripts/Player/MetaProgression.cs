using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MetaProgression
{
    // THESE REPRESENT META PROGRESSION MODIFERS TO THE PLAYER'S CORE STATS
    // SEE THE COMMENT NEXT TO EACH FOR INFORMATION ON HOW IT SHOULD BE APPLIED

    public static int addDamage { get; set; } // Damage added to the weapon's base damage before any multipliers are applied
    public static float multDamage; // Damage multiplier that is added with all other damage multipliers before being applied
    public static float attackSpeed; // The final attack speed number represents attacks per second.
    public static float bonusAttacks; // How many extra attack projectiles are spawned. Increases by less than a whole number will trigger ocassional attacks.

    public static float attackRange; // The distance a projectile can travel or the distance a melee attack can reach. 
    public static float attackArea; // Increases the size of projectiles and the width or degree angle of melee attacks.
    public static float attackLifespan; // Extends how long an attack or effect will last before fading
    public static float attackVelocity; // How quickly an attack projectile travels

    public static int maxHP;
    public static int regenHP;
    
    public static float moveSpeed;
    public static int bonusEXP;
    public static int bonusMoney;
    public static int pickupRange;
}
