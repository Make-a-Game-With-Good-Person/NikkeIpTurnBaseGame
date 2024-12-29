using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPassFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;
    public TurnPassFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        returnDecision.type = ReturnDecision.DecisionType.Pass;
        return returnDecision;
    }
}
