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
        isBuffSkill = true;
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
        battleManager.curControlUnit[EStatType.ATK] += plusATK;
        Debug.Log($"은화의 공격력이 {DefaultATK}에서 {battleManager.curControlUnit[EStatType.ATK]}만큼 증가");
        yield return new WaitForSeconds(1f);
        changeStateWhenActEnd?.Invoke();
    }

    void ResetStat()
    {
        if (!buffOn) return;
        buffDur--;
        Debug.Log($"남은 버프턴 {buffDur}");
        if(buffDur <= 0)
        {
            buffDur = 1;
            eunhwa[EStatType.ATK] -= plusATK;
            Debug.Log($"은화의 공격력이 {eunhwa[EStatType.ATK]}로 롤백");
            buffOn = false;
        }
    }
}
