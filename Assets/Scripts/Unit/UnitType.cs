using System;

[Flags]
public enum UnitType
{
    None = 0,
    Playable = 1,
    Enemy = 2,
    Boss = 4
}