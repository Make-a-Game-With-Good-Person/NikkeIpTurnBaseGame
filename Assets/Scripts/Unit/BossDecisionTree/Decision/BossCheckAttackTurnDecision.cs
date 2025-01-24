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
            Debug.Log($"보스가 공격이 가능해 {_trueNode}로 보냄");
            return _trueNode;
        }
        else
        {
            Debug.Log($"보스가 공격이 불가능해 {_falseNode}로 보냄");
            return _falseNode;
        }
    }
}
