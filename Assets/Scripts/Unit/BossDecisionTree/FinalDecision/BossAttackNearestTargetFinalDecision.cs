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
        if (targets.Count == 0) Debug.Log("�ƹ��͵� ���޾ƿ�");
        float dist = float.MaxValue;

        // ���� ����� ���� Ÿ���ϵ��� ����
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
            Debug.Log("���� " + returnDecision.target.gameObject + "�� ���� ����� Ÿ������ ����");
            owner.selectedTarget = returnDecision.target.gameObject;
            returnDecision.type = ReturnDecision.DecisionType.NearTargetAttack;
        }
        else
        {
            Debug.Log("SelectAbilityDecision���� Ÿ���� ���� ����");
        }
    }
}
