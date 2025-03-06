using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=m0Ik1K02xfo&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=4

// TileXY - Represents the unity cartesian coordinates
// TileGrid - Represents the world tile grid coordinates

public class WorldScrolling : MonoBehaviour
{
    Transform playerTransform;
    Vector2Int currentTilePosition = new Vector2Int(1,1);
    [SerializeField] Vector2Int playerTileCoordinates;
    [SerializeField] float tileSize;
    GameObject[,] terrainTiles;

    [SerializeField] int terrainTileHorizontalCount;
    [SerializeField] int terrainTileVerticalCount;

    // This represents how many tiles the player can see at any one time. Default to 3 (1 for the center tile, and 2 for the tile on each side)
    [SerializeField] int fieldOfVisionHeight = 3;
    [SerializeField] int fieldOfVisionWidth = 3;


    private void Awake()
    {   // Intializes an array of terrain tiles to be filled on start by the Add function
        terrainTiles = new GameObject[terrainTileHorizontalCount, terrainTileVerticalCount];
    }


    private void Start()
    {
        playerTransform = GameManager.instance.playerTransform;
        // UpdateTilesOnScreen();
    }


    private void Update()
    {
        // Determines which tile location the player is currently on
        playerTileCoordinates.x = TileGridPosition(playerTransform.position.x);
        playerTileCoordinates.y = TileGridPosition(playerTransform.position.y);
        
        // Checks if the player has left the current tile. Then update the tiles shown on screen.
        if (currentTilePosition != playerTileCoordinates)
        {
            // Set the player's new tile position to be the current tile position
            currentTilePosition = playerTileCoordinates;

            // Update the tiles shown on screen
            UpdateTilesOnScreen();
        }
    }


    // This function is very self-explanatory compared to the others. When the player is determined to have moved to a new tile, the tiles around them need to be updated.
    private void UpdateTilesOnScreen()
    {
        for (int pov_x = 0; pov_x < fieldOfVisionWidth; pov_x++)
        {
            for (int pov_y = 0; pov_y < fieldOfVisionHeight; pov_y++)
            {
                // Debug.Log("Update Tile (X,Y): (" + pov_x + "," + pov_y + ")");

                int tileToUpdate_x = DetermineUpdateTile(playerTileCoordinates.x + pov_x, true);
                int tileToUpdate_y = DetermineUpdateTile(playerTileCoordinates.y + pov_y, false);

                GameObject tile = terrainTiles[tileToUpdate_x, tileToUpdate_y];

                // Makes sure a tile was actually found.
                if (tile != null)
                {
                    Vector3 newPosition = CalculateTileXYCoordinates(playerTileCoordinates.x + pov_x, playerTileCoordinates.y + pov_y);
                    Vector3 oldPosition = tile.transform.position;

                    // Only move the tile and spawn new objects if the tile is going to be moved to a new position.
                    if (newPosition != oldPosition)
                    {
                        tile.transform.position = newPosition; // Move tile to new location
                        tile.GetComponent<TerrainTile>().SpawnObjects(); // Run spawn script on objects within that tile
                    }
                }
                else
                {
                    Debug.Log("Unable to access the tile object due to null error");
                }
            }
        }
    }


    // This function determines where on the tile grid the player is based on their xy coordinates. Takes one axis at a time.
    // The grid referenced here extends beyond the limits of the tile count. Literally what grid tile in the entire world is the player currently on.
    private int TileGridPosition(float position)
    {
        int location;
        if (position > (-0.5f * tileSize) && position < (0.5f * tileSize)) // this is our edge case scenario for when the player is within the center tile of the arena
        {
            location = -1;
        }
        else // This is applicable to any other scenario
        {
            location = (int)((position - (0.5f * tileSize)) / tileSize);
            location -= location < 0 ? 1 : 0; // Necessary for negative values
        }

        return location;
    }


    // This function determines which tile needs to be used based on the tile coordinates.
    private int DetermineUpdateTile(float tileArrayCoordinate, bool horizontal)
    {
        float tilesFromOrigin;
        int axisTileCount;
        
        // Check if we need to use the horizontal or vertical tile count
        if (horizontal) axisTileCount = terrainTileHorizontalCount;
        else axisTileCount = terrainTileVerticalCount;

        // This uses the player position to calculate how many tiles away from the origin tile the player is. 
        if (tileArrayCoordinate >= 0)
        {
            tilesFromOrigin = tileArrayCoordinate % axisTileCount;
        }
        else
        {
            tileArrayCoordinate += 1;
            tilesFromOrigin = axisTileCount - 1 + tileArrayCoordinate % axisTileCount;
        }

        return (int)tilesFromOrigin;
    }


    // This function determines the cartesisan xy coordinates the tile needs to be teleported to.
    private Vector3 CalculateTileXYCoordinates(int x, int y)
    {   // Subtracting by half the width of a tile appropriately lines up the tiles with the current layout
        return new Vector3(x * tileSize, y * tileSize, 0f);
    }


    // This adds the tiles to the array. It is called on start by each child tile of this object.
    public void Add(GameObject tileGameObject, Vector2Int tilePosition)
    {   
        terrainTiles[tilePosition.x, tilePosition.y] = tileGameObject;
    }
}
