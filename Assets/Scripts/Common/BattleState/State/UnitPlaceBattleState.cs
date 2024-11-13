using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유닛 선택 하는 상태
/// UnitSelectState
/// </summary>
public class UnitPlaceBattleState : BattleState
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
    public void OnGameStartButton()
    {
        owner.stateMachine.ChangeState<GameStartBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState() 
    {
        yield return null;
        //유닛 선택 하기
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
