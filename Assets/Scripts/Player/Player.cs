using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    public CoreStats coreStats;

    // The player's max stats
    public int maxHealth;
    public int healthRegen;
    public int armor = 0;

    // "Current" stats
    public int currentHealth;

    public bool isDead;

    [SerializeField] StatusBar healthBar; // (static) set in inspector
    [HideInInspector] public Level level; // (dynamic) set on awake
    [HideInInspector] public Money money; // (dyanmic) set on awake

    // player controller global variables

    Rigidbody2D myRB;

    Vector2 moveDirection; // Holds the direction of the player's movement
    public float moveSpeed;
    bool moving = false;

    [HideInInspector] public Vector2 aimDirection; // Holds the direction of the player's aim
    public Transform aimTransform; // holds reference to the aim base object
    public Transform playerArtTransform; // holds reference to the player art object
    [HideInInspector] public bool aiming = false;

    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    [SerializeField] private UnityEvent pauseGameEvent;


    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        coreStats = new CoreStats(0,0,0,0,0,0,0);
        level = GetComponent<Level>();
        money = GetComponent<Money>();
        isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        myRB = GetComponent<Rigidbody2D>();
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;
    }

    // FixedUpdate is called once per "physics frame"
    private void FixedUpdate()
    {
        if (moving)
        {
            PlayerMovement();
        }

        if (aiming)
        {
            lastHorizontalVector = aimDirection.x;
            lastVerticalVector = aimDirection.y;
        }
    }

    // HEALTH FUNCTIONS ==========================================================================================

    public void TakeDamage(int damage) // Takes a positive damage number
    {
        if (isDead) return;
        
        ApplyArmor(ref damage);
        
        currentHealth -= damage; // Damages the player
        healthBar.SetState(currentHealth, maxHealth); // Updates the player's hp bar
        if (currentHealth <= 0) // GAME OVER state
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
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

    // HELPER FUNCTIONS =============================================================================

    // Applies the movement force to the player
    void PlayerMovement()
    {
        // Moves the player
        myRB.AddForce(new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed));

        // Flips the player
        if (moveDirection.x >= 0)
        {
            playerArtTransform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            playerArtTransform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Set's the player's speed based on their class
    public void SetBaseSpeed(float speed)
    {
        moveSpeed = speed;
    }


    // PLAYER INPUT PROCESSING =====================================================================

    // Captures the player's intended movement direction and if they're currently trying to move.
    public void OnMove(InputValue moveValue)
    {
        moveDirection = moveValue.Get<Vector2>().normalized;

        // Determines if the player is currently attempting to move
        if (Mathf.Abs(moveDirection.magnitude) > 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    // Captures the player's current aiming direction
    public void OnAim(InputValue aimValue)
    {
        aimDirection = aimValue.Get<Vector2>().normalized;

        if (Mathf.Abs(aimDirection.magnitude) > 0)
        {
            aiming = true;
            aimTransform.right = -aimDirection; // I have no idea why it has to be written like this to work, but so be it!
            // shipAimTransform.up = aimDirection; // rotates the player in the direction of their aim stick
        }
        else
        {
            aiming = false;
        }

    }

    public void OnPause(InputValue escValue)
    {
        pauseGameEvent.Invoke();
    }
}
