using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 예상 피해량등을 보여주는 상태
/// <para>요구사항 1. 카메라를 숄더뷰로 변경</para>
/// <para>요구사항 2. 범위 공격이어서 공격 가능 대상이 여러명일 경우에는 옆 버튼을 눌러서 예상 피해량을 각각 보여줌</para>
/// <para>요구사항 3. 뒤로가기 버튼으로 AbilityTargetBattleState로 복귀</para>
/// <para>요구사항 4. 확인 버튼으로 PerformAbilityBattleState로 진행</para>
/// <para>요구사항 5. 단일 대상 공격일 경우 옆 버튼으로 공격할 유닛 변경 가능</para>
/// <para>요구사항 6. 공격 애니메이션과 상대의 피격 애니메이션 재생</para>
/// </summary>
public class ConfirmAbilityTargetBattleState : BattleState
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
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
