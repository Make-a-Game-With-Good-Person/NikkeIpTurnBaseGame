using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillEmma : UnitSkill
{
    [SerializeField] Unit target;
    [SerializeField] float minusRange;
    int deBuffDur;
    bool deBuffOn;
    public float DefaultRange;

    protected override void Start()
    {
        base.Start();
        deBuffDur = 1;
        battleManager.EnemyRoundEndEvent.AddListener(ResetStat);
    }

    public override void Action()
    {
        base.Action();
        target = battleManager.selectedTarget.GetComponent<Unit>();
        DefaultRange = target[EStatType.Visual];
        minusRange = DefaultRange / 2;
        StartCoroutine(SkillAction());
    }
    IEnumerator SkillAction()
    {
        deBuffOn = true;
        target[EStatType.Visual] -= minusRange;
        Debug.Log($"{target.name}�� �����Ÿ��� {target[EStatType.Visual]}�� ����");
        yield return new WaitForSeconds(1f);
        changeStateWhenActEnd?.Invoke();
    }

    void ResetStat()
    {
        if (!deBuffOn) return;
        deBuffDur--;
        Debug.Log($"���� ������ {deBuffDur}");
        if (deBuffDur <= 0)
        {
            deBuffDur = 1;
            target[EStatType.Visual] += minusRange;
            Debug.Log($"{target.name}�� �����Ÿ��� {target[EStatType.Visual]}�� �ѹ�");
            deBuffOn = false;
        }
    }
}
