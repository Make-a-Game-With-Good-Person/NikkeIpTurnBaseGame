using UnityEngine;
public class GridNode
{
    public Vector3 position;
    public Vector2 size;
    public bool isOccupied;
    public bool isWalkable;
    public GridNode(Vector3 position, Vector2 size)
    {
        this.position = position;
        this.size = size;
        this.isOccupied = false;
        this.isWalkable = false;
        this.size = size;
    }
}
