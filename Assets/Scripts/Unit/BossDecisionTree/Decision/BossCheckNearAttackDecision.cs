using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class BossCheckNearAttackDecision : Decision
{
    Boss boss;

    public BossCheckNearAttackDecision(Boss boss, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.boss = boss;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (boss.unitSkills[1].GetComponent<BossAttackSkill>().curCoolTime == 0)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
