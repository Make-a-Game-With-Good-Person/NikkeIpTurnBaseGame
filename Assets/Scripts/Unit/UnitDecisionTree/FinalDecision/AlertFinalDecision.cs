using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AlertFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;

    public AlertFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        owner.curControlUnit.transform.rotation = Quaternion.Euler(0, owner.curControlUnit.transform.eulerAngles.y + 90, 0);
        returnDecision.type = ReturnDecision.DecisionType.Pass;
        return returnDecision;
    }
}
