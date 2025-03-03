using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePosition; // Manually set these in relation to the center. (Increments by 1)
    [SerializeField] List<SpawnObject> objectsToSpawn; // List of objects that get called when the tile gets moved
    List<GameObject> cleanupList; // List of objects spawned last time this was called. They will all need to be destroyed first.
    // Above variable (cleanupList) doesn't appear to be implemented as of yet.

    private void Start()
    {
        GetComponentInParent<WorldScrolling>().Add(gameObject, tilePosition);

        transform.position = new Vector3(-1000, -1000, 0);
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            objectsToSpawn[i].Spawn();
        }
    }
}
