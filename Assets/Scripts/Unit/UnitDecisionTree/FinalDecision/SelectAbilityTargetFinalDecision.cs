using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class SelectAbilityTargetFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;
    TargetFinder targetFinder;
    List<Unit> targets;

    public SelectAbilityTargetFinalDecision(ReturnDecision returnDecision, BattleManager owner, TargetFinder targetFinder)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
        this.targetFinder = targetFinder;
    }

    public override ReturnDecision Execute()
    {
        SelectTarget();
        return returnDecision;
    }

    void SelectTarget()
    {
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[0];
        /*owner.selectedSkillRangeTile = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, (from, to) =>
        { return from.distance + 1 <= owner.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= owner.curSelectedSkill.skillHeight; }
        );

        HashSet<Vector2Int> temp = owner.selectedSkillRangeTile.ToHashSet();*/
        owner.selectedSkillRangeTile = owner.curSelectedSkill.GetSkillRange();

        targets = targetFinder.FindTargets(owner.selectedSkillRangeTile);
        if (targets.Count == 0) Debug.Log("�ƹ��͵� ���޾ƿ�");
        float dist = float.MaxValue;

        // ����� ���� ����� ���� Ÿ���ϵ��� ����
        for(int i = 0; i < targets.Count; i++)
        {
            float targetDist = Vector3.Distance(owner.curControlUnit.transform.position, targets[i].transform.position);

            if(dist > targetDist)
            {
                dist = targetDist;
                returnDecision.target = targets[i];
            }
        }

        if(returnDecision.target != null)
        {
            Debug.Log("���� " + returnDecision.target.gameObject + "�� Ÿ������ ����");
            owner.selectedTarget = returnDecision.target.gameObject;
            returnDecision.type = ReturnDecision.DecisionType.Action;
        }
        else
        {
            Debug.Log("SelectAbilityDecision���� Ÿ���� ���� ����");
        }
    }
}
