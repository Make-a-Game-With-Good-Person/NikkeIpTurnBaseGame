using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckSummonDecision : Decision
{
    Boss boss;

    public BossCheckSummonDecision(Boss boss, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.boss = boss;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (!boss.unitSkills[2].GetComponent<BossSkill>().coolCheck)
        {
            Debug.Log("소환 가능!");
            return _trueNode;
        }
        else
        {
            Debug.Log("소환 불가능!");
            return _falseNode;
        }
    }
}
