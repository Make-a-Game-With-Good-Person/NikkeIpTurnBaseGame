using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 현재 선택한 적 유닛의 상태가 추적중인지 아닌지 확인 후
/// 추적 중이라면 타겟을 공격할 수 있는 위치로 이동하는 리프노드로
/// 아니라면 엄폐할 수 있는 최적의 위치로 이동하는 리프 노드로 보내는 결정 노드
/// </summary>
public class CheckChasingStateDecision : Decision
{
    BattleManager owner;

    public CheckChasingStateDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) :
        base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if(owner.curControlUnit.unitState == EUnitState.CHASING)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
