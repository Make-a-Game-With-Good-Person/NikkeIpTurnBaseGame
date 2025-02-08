using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillEunhwa : UnitSkill
{
    [SerializeField] Unit eunhwa;
    [SerializeField] float plusATK;
    int buffDur;
    bool buffOn;
    public float DefaultATK;

    protected override void Start()
    {
        base.Start();
        buffDur = 1;
        battleManager.PlayerRoundEndEvent.AddListener(ResetStat);
    }

    public override void Action()
    {
        base.Action();
        DefaultATK = battleManager.curControlUnit[EStatType.ATK];
        StartCoroutine(SkillAction());
    }
    IEnumerator SkillAction()
    {
        buffOn = true;
        battleManager.curControlUnit[EStatType.ATK] = plusATK;
        yield return new WaitForSeconds(1f);
        changeStateWhenActEnd?.Invoke();
        yield return new WaitForEndOfFrame();

        battleManager.curControlUnit.attackable = true; // юс╫ц
    }

    void ResetStat()
    {
        if (!buffOn) return;
        buffDur--;
        if(buffDur <= 0)
        {
            buffDur = 1;
            eunhwa[EStatType.ATK] = DefaultATK;
            buffOn = false;
        }
    }
}
