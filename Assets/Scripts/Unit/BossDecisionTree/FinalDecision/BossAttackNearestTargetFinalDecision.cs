using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackNearestTargetFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;
    List<Unit> targets;

    public BossAttackNearestTargetFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        SelectTarget();
        return returnDecision;
    }
    void SelectTarget()
    {
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[1];
        owner.selectedSkillRangeTile = owner.curSelectedSkill.GetSkillRange();

        targets = owner.curSelectedSkill.targetFinder.FindTargets(owner.selectedSkillRangeTile);
        if (targets.Count == 0) Debug.Log("아무것도 못받아옴");
        float dist = float.MaxValue;

        // 가장 가까운 적을 타겟하도록 설정
        for (int i = 0; i < targets.Count; i++)
        {
            float targetDist = Vector3.Distance(owner.curControlUnit.transform.position, targets[i].transform.position);

            if (dist > targetDist)
            {
                dist = targetDist;
                returnDecision.target = targets[i];
            }
        }

        if (returnDecision.target != null)
        {
            Debug.Log("적이 " + returnDecision.target.gameObject + "를 가장 가까운 타겟으로 지정");
            owner.selectedTarget = returnDecision.target.gameObject;
            returnDecision.type = ReturnDecision.DecisionType.NearTargetAttack;
        }
        else
        {
            Debug.Log("SelectAbilityDecision에서 타겟을 잡지 못함");
        }
    }
}
