using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동가능한 반경을 보여주는 상태
/// <para>요구사항 1. 유닛의 이동거리, 점프 능력치에 따라서 갈 수 있는 타일을 정해줄 것</para>
/// <para>요구사항 2. 뒤로가기 버튼으로 UnitSelectBattleState로 복귀</para>
/// <para>요구사항 3. 이동 확인 버튼으로 MoveSequenceState로 진행</para>
/// </summary>
public class MoveTargetBattleState : BattleState
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
