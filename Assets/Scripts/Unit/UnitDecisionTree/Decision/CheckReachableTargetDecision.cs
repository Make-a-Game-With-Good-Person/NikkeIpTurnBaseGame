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
            Debug.Log("Ʈ�� ���� �������");
            return _trueNode;
        }
        else
            return _falseNode;
    }

    bool FindTargetCount()
    {
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[0];
        owner.selectedSkillRangeTile = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, (from, to) =>
        { return from.distance + 1 <= owner.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= owner.curSelectedSkill.skillHeight; }
        );
        
        HashSet<Vector2Int> temp = owner.selectedSkillRangeTile.ToHashSet();

        if(targetFinder.FindTargetCount(temp) > 0)
        {
            Debug.Log("������ ���ƿ�");
            return true;
        }
        else
        {
            return false;
        }
    }

}
