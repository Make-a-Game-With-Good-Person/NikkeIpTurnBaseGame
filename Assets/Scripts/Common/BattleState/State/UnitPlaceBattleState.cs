using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유닛 선택 하는 상태
/// UnitSelectState
/// <para>요구사항 1. 플레이어가 보유한 유닛들을 보여주는 UI 활성화</para>
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
        //owner.UnitSelectUIController.GameStartButton.onClick.AddListener(OnGameStartButton);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        //owner.UnitSelectUIController.GameStartButton.onClick.RemoveListener(OnGameStartButton);
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
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState() 
    {
        yield return null;
        //UI띄움 등등
        

        //테스트용 임시 코드
        OnGameStartButton();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
