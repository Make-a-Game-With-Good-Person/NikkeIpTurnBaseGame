using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���ط����� �����ִ� ����
/// <para>�䱸���� 1. ī�޶� ������ ����</para>
/// <para>�䱸���� 2. ���� �����̾ ���� ���� ����� �������� ��쿡�� �� ��ư�� ������ ���� ���ط��� ���� ������</para>
/// <para>�䱸���� 3. �ڷΰ��� ��ư���� AbilityTargetBattleState�� ����</para>
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
