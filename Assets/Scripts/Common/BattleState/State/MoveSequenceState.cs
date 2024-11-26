using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �̵��ϴ� ����
/// <para>�䱸���� 1. ���ֵ��� �ִϸ��̼� Ȱ��ȭ</para>
/// <para>�䱸���� 2. �������鼭 ���ڵ��� Ȱ��ȭ �Ǹ� ó��</para>
/// <para>�䱸���� 3. ��� ���� ������ �������� �´°� ó��</para>
/// <para>�䱸���� 4. �������� SelectUnitBattleState ����</para>
/// </summary>
public class MoveSequenceState : BattleState
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
        UnitMovement movement = owner.curControlUnit.GetComponent<UnitMovement>();

        yield return StartCoroutine(movement.Traverse(owner.tile.coordinate, owner.tileManager));

        //owner.curControlUnit.movable = false;

        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
