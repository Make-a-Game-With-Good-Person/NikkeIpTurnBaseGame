using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 능력의 목표를 선택하는 상태
/// <para>요구사항 1. 공격가능한 타일 표시</para>
/// <para>요구사항 2. 공격 대상이 있을 때 선택 버튼을 누르면 ConfirmAbilityTargetBattleState로 진행</para>
/// <para>요구사항 3. 뒤로가기 버튼을 눌렀을 때 UnitSelectBattleState로 복귀</para>
/// <para>요구사항 4. 공격 대상이 될만한 유닛이나 물건들을 빨간색으로든 뭐든 표시해줌</para>
/// <para>요구사항 5. 공격이 단일 대상 공격이면 옆 버튼으로 공격할 유닛 선택 가능</para>
/// </summary>
public class AbilityTargetBattleState : BattleState
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
