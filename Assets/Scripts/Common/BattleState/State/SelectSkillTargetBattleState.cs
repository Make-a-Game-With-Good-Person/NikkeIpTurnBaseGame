using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �� ���¿��� ������ ��ų�� �� Ÿ���� ���ϴ� ����.
/// <para>�䱸���� 1. ���ݰ����� ��� ǥ��</para>
/// <para>�䱸���� 2. ���� ����� Ŭ���ϸ� ConfirmAbilityTargetBattleState</para>
/// <para>�䱸���� 3. �ڷΰ��� ��ư�� ������ �� AbilityTargetBattleState ����</para>
/// <para>�䱸���� 4. ���� ������ ������ �ľ��� ���� ������ ���� ��Ų��.</para>
/// 
/// </summary>
public class SelectSkillTargetBattleState : BattleState
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
    //�䱸���� 1
    
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
        owner.curState = BATTLESTATE.SELECTSKILLTARGET;
        
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        
    }
    #endregion
    #endregion

    #region EventHandlers
    //�䱸���� 2
    private void OnConfirmButton()
    {
        //owner.abilitytargets = ���õ�����.���õȴɷ�.�ɷ¹������() ������ ��� ��ȯ
        //owner.stateMachine.ChangeState<ConfirmAbilityBattleState>();
    }
    //�䱸���� 3
    private void OnCancelButton()
    {
        //Ȯ�ΰ� �ڷ� ��ư UI ��Ȱ��ȭ
        owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //�䱸���� 5
    private void OnChangeTargetCommand()
    {
        //������ �ɷ��� ���ϴ���� ��쿡�� �۵�
        //���� ������ ȭ��ǥ�� ����� �� ������ �����ϴ� �Լ�
        //���õ� ������ ������ �����ϰ� �ٲ������
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //ShowTargetableTiles();
        // ���⼭ curControlUnit�� index�� �����ؼ� ������ ��ų �������� Ȱ��ȭ �Ѵ�.
        // �� �� 
        // curControlUnit�� index�� �����ؼ� �� ��ų���� ������ image sprite�� �ȿ� ������� �����Ѵ�.

        //Ȯ�ΰ� �ڷ� ��ư UI Ȱ��ȭ
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
