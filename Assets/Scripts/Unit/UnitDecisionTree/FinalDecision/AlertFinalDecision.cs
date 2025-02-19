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
        returnDecision.type = ReturnDecision.DecisionType.Alert;
        return returnDecision;
    }
}
