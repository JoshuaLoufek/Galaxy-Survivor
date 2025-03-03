using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LayerMask layersToHit;
    [SerializeField] float maxDistance = 50f;
    [SerializeField] float maxWidth = 5f;
    [SerializeField] Transform aimTransform;
    [SerializeField] SpriteRenderer laserRenderer;



    void Update()
    {
        float angle = aimTransform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // Check for collisions with the intended path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, maxDistance, layersToHit);
        if(hit.collider == null)
        {
            transform.localScale = new Vector3(maxDistance, transform.localScale.y, 1);
            return;
        }

        // Adjust the size and length of the laser
        // transform.localScale = new Vector3(maxDistance, transform.localScale.y, 1);
        laserRenderer.size += new Vector2(maxWidth, maxDistance);
        Debug.Log(hit.collider.gameObject.name);
        
        // now for enemy hit detection
    }
}
