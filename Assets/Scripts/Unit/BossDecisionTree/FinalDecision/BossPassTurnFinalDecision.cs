using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPassTurnFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;

    public BossPassTurnFinalDecision(ReturnDecision returnDecision)
    {
        this.returnDecision = returnDecision;
    }

    public override ReturnDecision Execute()
    {
        returnDecision.type = ReturnDecision.DecisionType.Pass;
        return returnDecision;
    }
}
