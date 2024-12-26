using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
            owner.cameraStateController.SwitchToShoulderView(owner.curControlUnit.shoulder, owner.selectedTarget.transform);
        }
        else
        {
            EnemeyAddListeners();
        }
        
    }
    public override void Exit()
    {
        if (!owner.enemyTurn) base.Exit();
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

        owner.curSelectedSkill.Action(); // ���� ��� ���� ���... �� ������ �Լ��� ȣ��.
        owner.confirmAbilityTargetUIController.Hide();

    }
    #endregion

    #region MonoBehaviour
    #endregion
}
