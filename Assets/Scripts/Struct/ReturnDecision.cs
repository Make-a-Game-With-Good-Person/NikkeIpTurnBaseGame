using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ReturnDecision
{
    public enum DecisionType
    {
        Action,
        Move,
        Pass,
        FarTargetAttack,
        NearTargetAttack,
        Summon,
        AttackPass,
        Alert
    }

    public DecisionType type;
    public Vector2Int pos;
    public Unit target;
}
