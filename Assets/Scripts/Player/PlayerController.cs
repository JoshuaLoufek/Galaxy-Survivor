using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // GLOBAL VARIABLES ==========================================================================================

    // Scripts and Game Objects
    [HideInInspector] public PlayerStats playerStats; // (dynamic) set on awake
    Rigidbody2D myRB;
    public Transform aimTransform; // holds reference to the aim base object
    public Transform playerArtTransform; // holds reference to the player art object

    // VARIABLES
    // Movement
    private Vector2 moveDirection; // Holds the direction of the player's movement
    public float baseMoveSpeed;
    public float moveSpeed;
    [HideInInspector] public bool moving = false;
    // Aim
    [HideInInspector] public Vector2 aimDirection; // Holds the direction of the player's aim
    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;
    [HideInInspector] public bool aiming = false;

    // Events
    [SerializeField] private UnityEvent pauseGameEvent;


    // INITIALIZATION FUNCTIONS ==================================================================================

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        myRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerController();
    }

    private void InitializePlayerController()
    {
        baseMoveSpeed = 30f;
        
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;

        CalculateMoveSpeed();
    }

    // UPDATE FUNCTIONS ==========================================================================================

    // FixedUpdate is called once per "physics frame"
    private void FixedUpdate()
    {
        if(moving)
        {
            PlayerMovement();
        }

        if(aiming)
        {
            lastHorizontalVector = aimDirection.x;
            lastVerticalVector = aimDirection.y;
        }
    }


    // HELPER FUNCTIONS ==========================================================================================

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


    // PLAYER INPUT PROCESSING ===================================================================================

    // Captures the player's intended movement direction and if they're currently trying to move.
    public void OnMove(InputValue moveValue) 
    {
        moveDirection = moveValue.Get<Vector2>().normalized;

        // Determines if the player is currently attempting to move
        if(Mathf.Abs(moveDirection.magnitude) > 0)
        {
            moving = true;
        } else
        {
            moving = false;
        }
    }

    // Captures the player's current aiming direction
    public void OnAim(InputValue aimValue)
    {
        aimDirection = aimValue.Get<Vector2>().normalized;

        if(Mathf.Abs(aimDirection.magnitude) > 0)
        {
            aiming = true;
            aimTransform.right = -aimDirection; // I have no idea why it has to be written like this to work, but so be it!
            // shipAimTransform.up = aimDirection; // rotates the player in the direction of their aim stick
        } else
        {
            aiming = false;
        }

    }

    public void OnPause(InputValue escValue)
    {
        pauseGameEvent.Invoke();
    }

    // STATISTIC CALCULATOR FUNCTIONS ============================================================================
        // This is where the final, usable versions of each stat are calculated.
        // They will be applied in the functions that are above.

    public void CalculateMoveSpeed()
    {
        moveSpeed = baseMoveSpeed * (1 + playerStats.moveSpeed);
        Debug.Log("Move Speed Set: " + moveSpeed);
    }
}
