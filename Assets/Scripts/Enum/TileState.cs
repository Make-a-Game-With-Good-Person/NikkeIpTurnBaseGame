using System;

[Flags]
[Serializable]
public enum TileState
{
    Walkable = 1,
    Selectable = 2
}