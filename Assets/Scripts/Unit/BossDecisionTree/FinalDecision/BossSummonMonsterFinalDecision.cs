using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonMonsterFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;
    public BossSummonMonsterFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[2];
        returnDecision.type = ReturnDecision.DecisionType.Summon;
        return returnDecision;
    }
}
