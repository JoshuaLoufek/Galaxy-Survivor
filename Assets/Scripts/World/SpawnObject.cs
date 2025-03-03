using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField][Range(0f,1f)] float probability;

    GameObject newObject = null;

    internal void Spawn()
    {
        // Destroy other objects that were spawned by this object
        if (newObject != null)
        {
            Destroy(newObject);
        }

        // Spawn the new object
        if (UnityEngine.Random.value < probability)
        {
            newObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
            newObject.transform.parent = transform; // has the object become a child of this object

        }
    }
}
