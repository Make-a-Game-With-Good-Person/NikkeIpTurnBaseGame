using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackFartherTargetFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;
    List<Unit> targets;

    public BossAttackFartherTargetFinalDecision(ReturnDecision returnDecision, BattleManager owner)
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
        owner.curSelectedSkill = owner.curControlUnit.unitSkills[0];
        owner.selectedSkillRangeTile = owner.curSelectedSkill.GetSkillRange();

        targets = owner.curSelectedSkill.targetFinder.FindTargets(owner.selectedSkillRangeTile);
        if (targets.Count == 0) Debug.Log("아무것도 못받아옴");
        float dist = float.MinValue;

        // 가장 먼 적을 타겟하도록 설정
        for (int i = 0; i < targets.Count; i++)
        {
            float targetDist = Vector3.Distance(owner.curControlUnit.transform.position, targets[i].transform.position);

            if (dist < targetDist)
            {
                dist = targetDist;
                returnDecision.target = targets[i];
            }
        }

        if (returnDecision.target != null)
        {
            Debug.Log("적이 " + returnDecision.target.gameObject + "를 가장 먼 타겟으로 지정");
            owner.selectedTarget = returnDecision.target.gameObject;
            returnDecision.type = ReturnDecision.DecisionType.FarTargetAttack;
        }
        else
        {
            Debug.Log("SelectAbilityDecision에서 타겟을 잡지 못함");
        }
    }
}
