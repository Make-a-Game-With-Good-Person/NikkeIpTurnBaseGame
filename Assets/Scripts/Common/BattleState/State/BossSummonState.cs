using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonState : BattleState
{
    protected override void AddListeners()
    {
        base.AddListeners();
        owner.curSelectedSkill.changeStateWhenActEnd.AddListener(ChangeState);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.curSelectedSkill.changeStateWhenActEnd.RemoveListener(ChangeState);
    }

    void ChangeState()
    {
        owner.stateMachine.ChangeState<PerformAbilityBattleState>();
    }

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(ProcessingState());
    }

    public override void Exit()
    {
        base.Exit();
    }

    private IEnumerator ProcessingState()
    {
        yield return null;
        owner.curSelectedSkill.Action(); // 공격 모션 판정 등등... 다 실행할 함수를 호출.
    }
}
