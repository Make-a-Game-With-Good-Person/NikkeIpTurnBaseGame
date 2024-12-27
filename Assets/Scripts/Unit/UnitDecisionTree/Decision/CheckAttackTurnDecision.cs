using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 선택한 적 유닛이 공격 턴을 보유하고 있는지 확인하는 결정 노드
/// 공격 턴을 보유하고 있다면 적 유닛의 스킬 범위 안에 타겟이 존재하는지 확인하는 결정 노드로
/// 보유하고 있지 않다면 이동 턴 보유 여부를 확인하는 결정 노드로 이동
/// </summary>
public class CheckAttackTurnDecision : Decision
{
    BattleManager owner;

    public CheckAttackTurnDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (owner.curControlUnit.attackable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
