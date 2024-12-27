using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선택한 적 유닛이 공격/이동 턴 중 하나라도 가지고 있는지 확인하는 결정 노드
/// 하나라도 존재한다면 공격 턴을 보유하고 있는지 확인하는 결정 노드로
/// 둘다 없다면 턴을 종료하는 리프 노드로 이동
/// </summary>
public class CheckTurnDecision : Decision
{
    BattleManager owner;

    public CheckTurnDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if(owner.curControlUnit.attackable || owner.curControlUnit.movable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
