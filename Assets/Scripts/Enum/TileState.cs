using System;

[Flags]
[Serializable]
public enum TileState
{
    Walkable = 1,
    Selectable = 2,
    Placeable = 4,
    Placed = 8
}
