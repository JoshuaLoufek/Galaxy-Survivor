using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    public void InitializeProjectile(Transform playerTransform, float x, float y);
}
