using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // GLOBAL VARIABLES =============================================================================

    Rigidbody2D myRB;
    
    Vector2 moveDirection; // Holds the direction of the player's movement
    public float moveSpeed;
    bool moving = false;

    [HideInInspector] public Vector2 aimDirection; // Holds the direction of the player's aim
    public Transform aimTransform; // holds reference to the aim base object
    public Transform playerArtTransform; // holds reference to the player art object
    public bool aiming = false;

    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;


    // CRITICAL UNITY FUNCTIONS =====================================================================

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

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
}
