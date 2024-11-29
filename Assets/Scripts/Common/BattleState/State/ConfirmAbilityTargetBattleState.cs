using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���ط����� �����ִ� ����
/// <para>�䱸���� 1. ī�޶� ������ ����</para>
/// <para>�䱸���� 2. ���� �����̾ ���� ���� ����� �������� ��쿡�� �� ��ư�� ������ ���� ���ط��� ���� ������</para>
/// <para>�䱸���� 3. �ڷΰ��� ��ư���� SelectSkillTargetBattleState�� ����</para>
/// <para>�䱸���� 4. Ȯ�� ��ư���� PerformAbilityBattleState�� ����</para>
/// <para>�䱸���� 5. ���� ��� ������ ��� �� ��ư���� ������ ���� ���� ����</para>
/// <para>�䱸���� 6. ���� �ִϸ��̼ǰ� ����� �ǰ� �ִϸ��̼� ���</para>
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
        owner.curSelectedSkill.Action(); // ���� ��� ���� ���... �� ������ �Լ��� ȣ��.
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
