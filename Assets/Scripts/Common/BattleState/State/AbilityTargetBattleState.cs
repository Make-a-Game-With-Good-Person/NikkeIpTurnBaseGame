using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

/// <summary>
/// 스킬의 스펙을 확인하고 스킬을 선택하는 상태
/// <para>요구사항 1. 공격가능한 타일 표시</para>
/// <para>요구사항 2. 공격 대상이 있을 때 선택 버튼을 누르면 ConfirmAbilityTargetBattleState로 진행</para>
/// <para>요구사항 3. 뒤로가기 버튼을 눌렀을 때 UnitSelectBattleState로 복귀</para>
/// <para>요구사항 4. 공격 대상이 될만한 유닛이나 물건들을 빨간색으로든 뭐든 표시해줌</para>
/// <para>요구사항 5. 공격이 단일 대상 공격이면 옆 버튼으로 공격할 유닛 선택 가능</para>
/// <para>각 스킬 아이콘을 누르면 스킬 별로 사정거리를 타일 색을 바꾸는걸로 표시해서 알려줌</para>
/// <para>스킬 아이콘을 누르고 확인을 누르면 다음 상태로 이전 >> 다음 상태에선 적을 선택하고 다음 상태로 넘어가면 그때 Confirm으로 간다.</para>
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
    private void ShowTargetableTiles() // 스킬 아이콘 버튼을 클릭하면 호출할 함수
    {
        UnitSkill selectedSkill = EventSystem.current.currentSelectedGameObject.GetComponent<UnitSkill>(); // 바로 직전에 클릭한 버튼을 불러옴
        owner.curSelectedSkill = selectedSkill;
        owner.selectedSkillRangeTile = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, (from, to) => 
        { return from.distance + 1 <= selectedSkill.skillRange && Math.Abs(from.height - to.height) <= selectedSkill.skillHeight; }
        );

        owner.tileManager.ShowTiles(owner.selectedSkillRangeTile);
    }
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        owner.abilityTargetUIController.cancelButton.onClick.AddListener(OnCancelButton);
        owner.abilityTargetUIController.confirmButton.onClick.AddListener(OnConfirmButton);

        int index = owner.curControlUnit.index % 10;

        for(int i = 0; i < owner.abilityTargetUIController.skillButtonList[index].buttonList.Count; i++)
        {
            owner.abilityTargetUIController.skillButtonList[index].buttonList[i].onClick.AddListener(ShowTargetableTiles);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.abilityTargetUIController.cancelButton.onClick.RemoveListener(OnCancelButton);
        owner.abilityTargetUIController.confirmButton.onClick.RemoveListener(OnConfirmButton);

        int index = owner.curControlUnit.index % 10;

        for (int i = 0; i < owner.abilityTargetUIController.skillButtonList[index].buttonList.Count; i++)
        {
            owner.abilityTargetUIController.skillButtonList[index].buttonList[i].onClick.RemoveListener(ShowTargetableTiles);
        }
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.curState = BATTLESTATE.ABILITYTARGET;
        owner.abilityTargetUIController.Display();
        owner.abilityTargetUIController.UISetting(owner.curControlUnit);
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        owner.abilityTargetUIController.ResetSkillUI(owner.curControlUnit);
        owner.abilityTargetUIController.Hide();
    }
    #endregion
    #endregion

    #region EventHandlers
    //요구사항 2
    private void OnConfirmButton()
    {
        //owner.abilitytargets = 선택된유닛.선택된능력.능력범위계산() 범위내 대상 반환
        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
    }
    //요구사항 3
    private void OnCancelButton()
    {
        //확인과 뒤로 버튼 UI 비활성화
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
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
