using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 보스가 공격이 가능할 때, 먼거리 타격 스킬이 사용 가능한지 체크하는 결정노드
/// 스킬이 사용 가능한지는 스킬의 쿨타임을 보고 판단한다.
/// </summary>
public class BossCheckFarAttackDecision : Decision
{
    Boss boss;

    public BossCheckFarAttackDecision(Boss boss, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.boss = boss;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (!boss.unitSkills[0].GetComponent<BossSkill>().coolCheck)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
