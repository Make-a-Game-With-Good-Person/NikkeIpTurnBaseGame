using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBattleState : BattleState
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
        StartCoroutine(Initializing());
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
    private IEnumerator Initializing() 
    {
        yield return null;
        //�ʷε�
        //���� �ε�
        //����
        //ī�޶� ���� ���
        owner.stateMachine.ChangeState<UnitPlaceBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
