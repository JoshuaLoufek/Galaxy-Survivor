using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    public virtual void InitializeProjectile(Transform origin, Vector2 fireDirection, WeaponBase weaponBase, PlayerStats stats)
    {

    }

    public abstract void MoveProjectile();
}
