using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAbilityTargetFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;

    public SelectAbilityTargetFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        return returnDecision;
    }
}
