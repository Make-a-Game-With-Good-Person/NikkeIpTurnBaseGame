using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 선택한 적 유닛이 이동턴을 보유하고 있는지 확인하는 결정 노드
/// 이동 턴을 보유하고 있다면 현재 적의 추적 상태를 확인하는 결정 노드로
/// 이동 턴이 없다면 턴을 종료하는 리프 노드로 이동
/// </summary>
public class CheckMoveTurnDecision : Decision
{
    BattleManager owner;

    public CheckMoveTurnDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode,falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (owner.curControlUnit.movable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
