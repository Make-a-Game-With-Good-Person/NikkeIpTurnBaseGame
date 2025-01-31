using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

/// <summary>
/// ������ �����Ƽ�� ���� ��¥�� ������
/// <para>�䱸���� 1. �����ϰ� ���� GameEnd üũ(������ ���� �׾����� �Ʊ��� ���� �׾����� ��)</para>
/// <para>�䱸���� 2. ���� �Ʊ� ������ ���� �� �������� �ʾ����� UnitSelectBattleState�� ����</para>
/// <para>�䱸���� 3. �Ʊ� ������ ���� �������� ���� owner�� ������ ������ ��</para>
/// <para>�䱸���� 4. �̹� ������ Ability ��ư�� ��Ȱ��ȭ�� ��</para>
/// </summary>
public class PerformAbilityBattleState : BattleState
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
        owner.curState = BATTLESTATE.PERFORMABILITY;
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
        owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);

        // ���⼭ �ʵ� ���� ������ ��� �׾����� üũ �� �׾��ٸ� ���������� �����ϸ� ��

        owner.curControlUnit.attackable = false; // ������ ������

        owner.stateMachine.ChangeState<TurnCheckBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
