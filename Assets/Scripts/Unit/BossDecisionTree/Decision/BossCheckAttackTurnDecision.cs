using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 보스가 현재 턴에서 '공격'이 가능한지 체크하는 결정노드
/// </summary>
public class BossCheckAttackTurnDecision : Decision
{
    Boss boss;

    public BossCheckAttackTurnDecision(Boss boss, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.boss = boss;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (boss.attackable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
