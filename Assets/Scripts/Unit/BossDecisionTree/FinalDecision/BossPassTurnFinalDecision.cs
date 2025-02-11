using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPassTurnFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    Boss boss;
    public BossPassTurnFinalDecision(ReturnDecision returnDecision, Boss boss)
    {
        this.returnDecision = returnDecision;
        this.boss = boss;
    }

    public override ReturnDecision Execute()
    {
        returnDecision.type = ReturnDecision.DecisionType.Pass;
        //CheckCoolTime();
        return returnDecision;
    }

    void CheckCoolTime()
    {
        for(int i = 0; i < boss.unitSkills.Count; i++)
        {
            boss.unitSkills[i].GetComponent<BossSkill>().CoolTimeCheck();
        }
    }
}
