using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillEmma : UnitSkill
{
    [SerializeField] Unit target;
    [SerializeField] float minusRange;
    int deBuffDur;
    bool deBuffOn;
    public float DefaultRragne;

    protected override void Start()
    {
        base.Start();
        deBuffDur = 1;
        battleManager.PlayerRoundEndEvent.AddListener(ResetStat);
    }

    public override void Action()
    {
        base.Action();
        target = battleManager.selectedTarget.GetComponent<Unit>();
        DefaultRragne = target[EStatType.Visual];
        StartCoroutine(SkillAction());
    }
    IEnumerator SkillAction()
    {
        deBuffOn = true;
        target[EStatType.Visual] = minusRange;
        yield return new WaitForSeconds(1f);
        changeStateWhenActEnd?.Invoke();
    }

    void ResetStat()
    {
        if (!deBuffOn) return;
        deBuffDur--;
        if (deBuffDur <= 0)
        {
            deBuffDur = 1;
            target[EStatType.Visual] = DefaultRragne;
            deBuffOn = false;
        }
    }
}
