using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// 플레이어가 선택한 유닛의 정보를 보여주는 상태
/// <para>요구사항 1. 유닛의 버프, 디버프 들을 눌렀을때 어떤 효과인지 확인 가능</para>
/// <para>요구사항 2. 빈곳을 눌렀을 때 UnitSelectBattleState로 복귀</para>
/// <para>UI를 키고 끄며, 특정 버튼을 누르면 전상태로 복귀하는 State</para>
/// </summary>
public class ShowUnitDetailBattleState : BattleState
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
        owner.unitdetailUIController.cancelEvent.AddListener(OnCancelButton);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.unitdetailUIController.cancelEvent.RemoveListener(OnCancelButton);
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
    //요구사항 2 구현
    private void OnCancelButton()
    {
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //요구사항 1 구현
        owner.unitdetailUIController.Display(owner.selectedTarget);
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
