using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

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
    void EnemeyAddListeners()
    {
        owner.curSelectedSkill.changeStateWhenActEnd.AddListener(ChangeState);
    }

    void EnemyRemoveListeners()
    {
        owner.curSelectedSkill.changeStateWhenActEnd.RemoveListener(ChangeState);
    }

    void OnCancelButton()
    {
        owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);
        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
    }

    void OnConfirmButton()
    {
        StartCoroutine(ProcessState());
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
        if (!owner.enemyTurn)
        {
            base.Enter();
            owner.confirmAbilityTargetUIController.Display();
            owner.confirmAbilityTargetUIController.SetExpectedText(owner.curControlUnit, owner.touchedUnit.GetComponent<Unit>());
            //owner.cameraStateController.SwitchToShoulderView(owner.curControlUnit.shoulder, owner.selectedTarget.transform);
            owner.cameraStateController.SwitchToQuaterView(owner.touchedUnit.transform);
            owner.curSelectedSkill.TurnOnTargetRange();
        }
        else
        {
            EnemeyAddListeners();
            StartCoroutine(ProcessState());
        }
        
    }
    public override void Exit()
    {
        if (!owner.enemyTurn)
        {
            base.Exit();
            owner.curSelectedSkill.TurnOffTargetRange();
        }
        else
        {
            EnemyRemoveListeners();
        }
        owner.confirmAbilityTargetUIController.Hide();
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
    private IEnumerator ProcessState()
    {
        yield return null;

        Vector3 targetPos = new Vector3(owner.selectedTarget.transform.position.x, owner.curControlUnit.transform.position.y, owner.selectedTarget.transform.position.z);
        Vector3 dir = (targetPos - owner.curControlUnit.transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        owner.curControlUnit.transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);

        yield return null;

        owner.curSelectedSkill.Action(); // 공격 모션 판정 등등... 다 실행할 함수를 호출.
        owner.confirmAbilityTargetUIController.Hide();

    }
    #endregion

    #region MonoBehaviour
    #endregion
}
