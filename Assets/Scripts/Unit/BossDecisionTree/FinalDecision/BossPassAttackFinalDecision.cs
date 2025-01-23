using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPassAttackFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;

    public BossPassAttackFinalDecision(ReturnDecision returnDecision)
    {
        this.returnDecision = returnDecision;
        returnDecision.type = ReturnDecision.DecisionType.AttackPass;
    }

    public override ReturnDecision Execute()
    {
        return returnDecision;
    }
}
