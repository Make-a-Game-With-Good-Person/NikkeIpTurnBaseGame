using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonMonsterFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;
    Boss boss;
    public BossSummonMonsterFinalDecision(ReturnDecision returnDecision, BattleManager owner, Boss boss)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
        this.boss = boss;   
    }

    public override ReturnDecision Execute()
    {
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[2];
        returnDecision.type = ReturnDecision.DecisionType.Summon;
        //CheckCoolTime();
        return returnDecision;
    }

    void CheckCoolTime()
    {
        for (int i = 0; i < boss.unitSkills.Count; i++)
        {
            boss.unitSkills[i].GetComponent<BossSkill>().CoolTimeCheck();
        }
    }
}
