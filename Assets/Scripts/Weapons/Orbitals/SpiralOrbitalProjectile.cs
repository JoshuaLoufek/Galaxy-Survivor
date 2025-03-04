using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpiralOrbitalProjectile : MonoBehaviour
{
    float radius; 
    float moveSpeed;
    float currentAngle;
    float startTime;
    public int damage = 5;
    public int pierce = 999;

    public float initialMoveSpeed;
    public float expansionSpeed;
    public float revolutionSpeed;

    Transform playerTransform;

    void Start()
    {
        radius = 1f;
        currentAngle = 0f;
        moveSpeed = initialMoveSpeed;
        startTime = Time.time;
    }

    void Update()
    {
        Version1();
        //Version2();
    }

    void Version1() // constant revolution speed
    {
        // Determine the position to move to
        float xPos = playerTransform.position.x + (radius * Mathf.Sin((Time.time - startTime) * revolutionSpeed));
        float yPos = playerTransform.position.y + (radius * Mathf.Cos((Time.time - startTime) * revolutionSpeed));

        // Move the projectile
        transform.position = new Vector2(xPos, yPos);

        // Update the radius for the next update
        radius += expansionSpeed * Time.deltaTime;
    }

    void Version2() // constant move speed (or can be manually tweaked)
    {
        // The radius increases at a constant rate
        radius += expansionSpeed * Time.deltaTime;
        moveSpeed += expansionSpeed * Time.deltaTime;

        // Solve for the change in angle if the projectile moves a certain distance
        float theta = (moveSpeed * Time.deltaTime) / radius; // As the radius increases, the angle moved decreases
        currentAngle += theta;

        // update the position of the projectile
        float xPos = playerTransform.position.x + Mathf.Sin(currentAngle) * radius;
        float yPos = playerTransform.position.y + Mathf.Cos(currentAngle) * radius;
        transform.position = new Vector2(xPos, yPos);
    }

    public void InitializeProjectile(Transform pt, float x, float y, int dmg)
    {
        playerTransform = pt;
        transform.position = new Vector2(pt.position.x, pt.position.y);
        damage = dmg;
    }

    // This function is called whenever the yoyo comes into contact with a (collider + rigidbody) object.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Grabs the damagable interface of the object hit
        IDamageable enemy = collision.GetComponent<IDamageable>();
        Debug.Log(enemy);
        // Verifies that the found object isn't null
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Damages the enemy
            pierce -= 1; // Reduces the pierce counter
            if (pierce <= 0) Destroy(gameObject); // Checks if pierce hit zero and the yoyo needs destroying
        }
    }
}
