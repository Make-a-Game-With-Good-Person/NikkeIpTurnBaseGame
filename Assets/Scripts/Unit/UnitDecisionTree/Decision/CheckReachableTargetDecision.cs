using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
/// <summary>
/// ������ �� ������ ��ų ���� �ȿ� ������ �� �ִ� ���� �ϳ��� �ִ��� Ȯ���ϴ� ���� ���
/// ���� ������ ���� �ִٸ� ���� �����ϴ� ���� ����
/// ���� ���ٸ� �̵� ���� Ȯ���ϴ� ���� ���� �̵�
/// </summary>
public class CheckReachableTargetDecision : Decision
{
    BattleManager owner;

    public CheckReachableTargetDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) :
        base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        owner.curControlUnit.unitState = EUnitState.CHASING;
        if (FindTargetCount())
        {
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

        if (owner.curSelectedSkill.targetFinder.FindTargetCount(owner.selectedSkillRangeTile) > 0)
        {
            Debug.Log("CheckReachableTargetDecision.FindTargetCount(), true");
            return true;
        }
        else
        {
            Debug.Log("CheckReachableTargetDecision.FindTargetCount(), false");
            return false;
        }
    }

}
