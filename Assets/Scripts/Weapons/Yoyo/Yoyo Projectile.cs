using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YoyoProjectile : MonoBehaviour, IProjectile
{
    Rigidbody2D myRB;
    public Transform playerTransform;


    public int damage;
    public int pierce = 999;
    public float initialVelocity = 50;
    public float returnForce = 100;
    public float returnAcceleration = 500;
    public int playerContact;

    void Awake()
    {
        myRB = GetComponent<Rigidbody2D>();
        playerContact = 0;
    }

    void Update()
    {
        yoyoMovement();
    }

    private void yoyoMovement()
    {
        float x = playerTransform.position.x - gameObject.transform.position.x;
        float y = playerTransform.position.y - gameObject.transform.position.y;

        returnForce += returnAcceleration * Time.deltaTime;

        myRB.AddForce(new Vector2(x, y).normalized * returnForce * Time.deltaTime);
    }

    public void InitializeProjectile(Transform pt, float x, float y)
    {
        playerTransform = pt;
        myRB.velocity = new Vector3(x, y).normalized * initialVelocity;
        transform.position = pt.position; // center the yoyo on the player to start
    }

    // This function is called whenever the yoyo comes into contact with a (collider + rigidbody) object.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Grabs the damagable interface of the object hit
        IDamageable enemy = collision.GetComponent<IDamageable>();

        // Verifies that the found object isn't null
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Damages the enemy
            pierce -= 1; // Reduces the pierce counter
            if (pierce <= 0) Destroy(gameObject); // Checks if pierce hit zero and the yoyo needs destroying
        }

        // Used so that the yoyo projectile is destroyed upon returning to the player
        PlayerStats playerHealth = collision.GetComponent<PlayerStats>();
        if (playerHealth != null)
        {
            if(playerContact > 0)
            {
                Destroy(gameObject);
            }
            playerContact += 1;
        }
    }
}
