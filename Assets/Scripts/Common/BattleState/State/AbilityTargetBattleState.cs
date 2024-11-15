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
    //요구사항 1
    private void ShowTargetableTiles()
    {
        //Unit selected = owner.units[owner.select];
        //HashSet<Vector2Int> movableTiles = owner.tileManager.SearchTile(selected.coord, 선택된유닛의 능력의 사거리 , 선택된 유닛의 능력의 사용가능 높이);
        //owner.tileManager.ShowTiles(movableTiles);
    }
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
        //owner.stateMachine.ChangeState<UnitSelectBattleState>();
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
        ShowTargetableTiles();
        //확인과 뒤로 버튼 UI 활성화
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
