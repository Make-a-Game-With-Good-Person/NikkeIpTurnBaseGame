using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ɷ��� ��ǥ�� �����ϴ� ����
/// <para>�䱸���� 1. ���ݰ����� Ÿ�� ǥ��</para>
/// <para>�䱸���� 2. ���� ����� ���� �� ���� ��ư�� ������ ConfirmAbilityTargetBattleState�� ����</para>
/// <para>�䱸���� 3. �ڷΰ��� ��ư�� ������ �� UnitSelectBattleState�� ����</para>
/// <para>�䱸���� 4. ���� ����� �ɸ��� �����̳� ���ǵ��� ���������ε� ���� ǥ������</para>
/// <para>�䱸���� 5. ������ ���� ��� �����̸� �� ��ư���� ������ ���� ���� ����</para>
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
