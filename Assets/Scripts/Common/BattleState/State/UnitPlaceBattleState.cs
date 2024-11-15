using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ���� �ϴ� ����
/// UnitSelectState
/// <para>�䱸���� 1. �÷��̾ ������ ���ֵ��� �����ִ� UI Ȱ��ȭ</para>
/// </summary>
public class UnitPlaceBattleState : BattleState
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
        //owner.UnitSelectUIController.GameStartButton.onClick.AddListener(OnGameStartButton);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        //owner.UnitSelectUIController.GameStartButton.onClick.RemoveListener(OnGameStartButton);
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
    public void OnGameStartButton()
    {
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState() 
    {
        yield return null;
        //UI��� ���
        

        //�׽�Ʈ�� �ӽ� �ڵ�
        OnGameStartButton();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}