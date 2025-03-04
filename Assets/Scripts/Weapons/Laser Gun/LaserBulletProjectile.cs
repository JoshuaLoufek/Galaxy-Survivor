using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletProjectile : MonoBehaviour
{
    // GLOBAL VARIABLES =============================================================================

    Rigidbody2D myRB;
    Vector3 direction;
    [SerializeField] float speed = 1f;
    [SerializeField] int damage = 1;
    [SerializeField] int pierce = 1; // Represents how many enemies the bullet can hit before being destroyed
    [SerializeField] float lifespan = 5f;

    // INITIALIZATION FUNCTIONS =====================================================================

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    public void InitializeProjectile(Transform pt, float x, float y, int dmg)
    {
        transform.position = pt.position; // center the bullet on the player to start

        // Sets the direction the bullet will travel in
        direction = new Vector3(x, y);

        // Rotates the bullet so it's aligned properly
        float z = -90 + (Mathf.Atan2(y, x) * (180 / Mathf.PI));
        transform.localRotation = Quaternion.Euler(0, 0, z);

        // Sets the damage (and other stats eventually)
        damage = dmg;
    }

    // UPDATE FUNCTION ==============================================================================

    void Update()
    {
        // Moves the bullet
        //transform.position += direction * speed * Time.deltaTime;
        BulletMovement();

        // Tracks time until the destruction of the object
        lifespan -= Time.deltaTime;
        if (lifespan <= 0) DestroySelf();
    }

    // HELPER FUNCTIONs =============================================================================

    private void BulletMovement()
    {
        myRB.velocity = direction * speed;
    }

    // This function is called whenever the bullet comes into contact with a (collider + rigidbody) object.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Grabs the enemy combat script
        IDamageable enemy = collision.GetComponent<IDamageable>();

        // Verifies that the found object was an enemy
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Damages the enemy
            pierce -= 1; // Reduces the pierce counter
            if (pierce <= 0) DestroySelf(); // Checks if pierce hit zero and the bullet needs destroying
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
