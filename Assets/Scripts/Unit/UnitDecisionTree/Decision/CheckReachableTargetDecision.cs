using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
/// <summary>
/// 선택한 적 유닛의 스킬 범위 안에 선택할 수 있는 적이 하나라도 있는지 확인하는 결정 노드
/// 공격 가능한 적이 있다면 적을 공격하는 리프 노드로
/// 적이 없다면 이동 턴을 확인하는 결정 노드로 이동
/// </summary>
public class CheckReachableTargetDecision : Decision
{
    BattleManager owner;
    TargetFinder targetFinder;

    public CheckReachableTargetDecision(BattleManager owner, TargetFinder targetFinder, DecisionTreeNode trueNode, DecisionTreeNode falseNode) :
        base(trueNode, falseNode)
    {
        this.owner = owner;
        this.targetFinder = targetFinder;
    }

    public override DecisionTreeNode GetBranch()
    {
        owner.curControlUnit.unitState = EUnitState.CHASING;
        if (FindTargetCount())
        {
            Debug.Log("트루 노드로 가세요라");
            return _trueNode;
        }
        else
            return _falseNode;
    }

    bool FindTargetCount()
    {
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[0];
        /*owner.selectedSkillRangeTile = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, (from, to) =>
        { return from.distance + 1 <= owner.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= owner.curSelectedSkill.skillHeight; }
        );
        
        HashSet<Vector2Int> temp = owner.selectedSkillRangeTile.ToHashSet();*/
        owner.selectedSkillRangeTile = owner.curSelectedSkill.GetSkillRange();

        if (targetFinder.FindTargetCount(owner.selectedSkillRangeTile) > 0)
        {
            Debug.Log("갯수가 많아요");
            return true;
        }
        else
        {
            Debug.Log("CheckReachableTargetDecision.FindTargetCount(), false");
            return false;
        }
    }

}
