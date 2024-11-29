using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 예상 피해량등을 보여주는 상태
/// <para>요구사항 1. 카메라를 숄더뷰로 변경</para>
/// <para>요구사항 2. 범위 공격이어서 공격 가능 대상이 여러명일 경우에는 옆 버튼을 눌러서 예상 피해량을 각각 보여줌</para>
/// <para>요구사항 3. 뒤로가기 버튼으로 SelectSkillTargetBattleState로 복귀</para>
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
    void OnCancelButton()
    {
        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
    }

    void OnConfirmButton()
    {
        owner.curSelectedSkill.Action(); // 공격 모션 판정 등등... 다 실행할 함수를 호출.
    }
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        owner.curSelectedSkill.changeStateWhenActEnd.AddListener(ChangeState);
        owner.confirmAbilityTargetUIController.cancelButton.onClick.AddListener(OnCancelButton);
        owner.confirmAbilityTargetUIController.confirmButton.onClick.AddListener(OnConfirmButton);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.curSelectedSkill.changeStateWhenActEnd.RemoveListener(ChangeState);
        owner.confirmAbilityTargetUIController.cancelButton.onClick.RemoveListener(OnCancelButton);
        owner.confirmAbilityTargetUIController.confirmButton.onClick.RemoveListener(OnConfirmButton);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.confirmAbilityTargetUIController.Display();
        owner.cameraStateController.SwitchToShoulderView(owner.curControlUnit.shoulder, owner.selectedTarget.transform);
        
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
    }

    public void ChangeState()
    {
        owner.stateMachine.ChangeState<PerformAbilityBattleState>();
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
