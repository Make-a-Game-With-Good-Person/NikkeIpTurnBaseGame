using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� �����ϰ� ���� �����ϴ� ����
/// <para>�䱸���� 1. ��ġ�� ��� ���� �ѷ��� �� ����</para>
/// <para>�䱸���� 2. �÷��̾� �����϶� �⺻������ �ϳ��� ������ ������ ���·� ����</para>
/// <para>�䱸���� 3. �÷��̾� �����϶� �ٸ� ������ ��ġ�ϸ� �� ������ �����ϴ� ������ �Ѿ</para>
/// <para>�䱸���� 4. ���� ������ �����ϸ� ShowUnitDetailBattleState�� ����</para>
/// <para>�䱸���� 5. �ؿ��� �� �� �ִ� �ൿ�� ǥ�õ�</para>
/// <para>�䱸���� 6. ���� ��ư�� ���� ��, AbilityTargetBattleState�� ����</para>
/// <para>�䱸���� 7. �̵� ��ư�� ���� ��, MoveTargetBattleState�� ����</para>
/// <para>�䱸���� 8. ������ �� �����ų� ������ ��ư�� ������, ShowUnitDetailBattleState�� ����</para>
/// <para>�䱸���� 9. ��� ��ư�� ���� ��, �ش� ������ ���� ����, ���� ���� �����ִ� ������ �ϳ��� �ڵ��̵�</para>
/// <para>�䱸���� 10. ������ ���ֱ��� ��������, ������� ������</para>
/// </summary>
public class UnitSelectBattleState : BattleState
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
    //�䱸���� 1�� ����
    private void OnDrag()
    {
        //�� �ѷ�������
    }
    // �䱸���� 3��, 4�� ����
    private void OnSelectUnit()
    {
        //if(������ �÷��̾� ��)
        //  owner.���� �����ϴ� ���� �ε��� = ������ ����
        //else ������ ������
        //  owner.���� ������ ���� = ������ ����
        //  owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
    }
    //�䱸���� 6�� ����
    private void OnAbilityButton()
    {
        //owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //�䱸���� 7�� ����
    private void OnMoveButton()
    {
        //owner.stateMachine.ChangeState<MoveTargetBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //�䱸���� 10�� ����
        //bool playerturn = owner.CheckTurn();
        bool playerturn = true;

        if (playerturn) 
        {
            //�䱸���� 2�� ���⿡ ����
            //�䱸���� 5�� ���⿡ ����
        }
        else 
        { 
            //��ǻ���� AI�� ȣ���ؼ� ����� ��
            //ChangeState�� ���⼭ ȣ��
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
