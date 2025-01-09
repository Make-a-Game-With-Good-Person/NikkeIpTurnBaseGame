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
        if (targets.Count == 0) Debug.Log("아무것도 못받아옴");
        float dist = float.MaxValue;

        // 현재는 가장 가까운 적을 타겟하도록 설정
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
            Debug.Log("적이 " + returnDecision.target.gameObject + "를 타겟으로 지정");
            owner.selectedTarget = returnDecision.target.gameObject;
            returnDecision.type = ReturnDecision.DecisionType.Action;
        }
        else
        {
            Debug.Log("SelectAbilityDecision에서 타겟을 잡지 못함");
        }
    }
}
