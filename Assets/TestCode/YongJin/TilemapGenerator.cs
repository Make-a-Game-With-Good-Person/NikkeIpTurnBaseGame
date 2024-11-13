using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapGenerator : MonoBehaviour
{
    public GameObject tilePrefab;    // Prefab of the tile
    public int mapWidth = 10;        // Number of tiles along the width
    public int mapHeight = 10;       // Number of tiles along the height
    public float tileSpacing = 1.0f; // Spacing between tiles

    void Start()
    {
        GenerateTileMap();
    }

    void GenerateTileMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                Vector3 position = new Vector3(x * tileSpacing, 0, z * tileSpacing);
                Instantiate(tilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}
