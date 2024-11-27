using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 전 상태에서 선택한 스킬을 쏠 타겟을 정하는 상태.
/// <para>요구사항 1. 공격가능한 대상 표시</para>
/// <para>요구사항 2. 공격 대상을 클릭하면 ConfirmAbilityTargetBattleState</para>
/// <para>요구사항 3. 뒤로가기 버튼을 눌렀을 때 AbilityTargetBattleState 복귀</para>
/// <para>요구사항 4. 공격 가능한 대상들을 파악해 몸을 빨갛게 점멸 시킨다.</para>
/// 
/// </summary>
public class SelectSkillTargetBattleState : BattleState
{
    #region Properties
    #region Private
    
    #endregion
    #region Protected
    #endregion
    #region public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    //요구사항 1
    
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.curState = BATTLESTATE.SELECTSKILLTARGET;
        
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        
    }
    #endregion
    #endregion

    #region EventHandlers
    //요구사항 2
    private void OnConfirmButton()
    {
        //owner.abilitytargets = 선택된유닛.선택된능력.능력범위계산() 범위내 대상 반환
        //owner.stateMachine.ChangeState<ConfirmAbilityBattleState>();
    }
    //요구사항 3
    private void OnCancelButton()
    {
        //확인과 뒤로 버튼 UI 비활성화
        owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //요구사항 5
    private void OnChangeTargetCommand()
    {
        //선택한 능력이 단일대상일 경우에만 작동
        //왼쪽 오른쪽 화살표로 대상이 될 유닛을 선택하는 함수
        //선택된 유닛의 정보를 간단하게 바꿔줘야함
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //ShowTargetableTiles();
        // 여기서 curControlUnit의 index를 참조해서 가능한 스킬 아이콘을 활성화 한다.
        // 할 일 
        // curControlUnit의 index를 참조해서 각 스킬들의 아이콘 image sprite랑 안에 스펙들을 조정한다.

        //확인과 뒤로 버튼 UI 활성화
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
