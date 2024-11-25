using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// �÷��̾ ������ ������ ������ �����ִ� ����
/// <para>�䱸���� 1. ������ ����, ����� ���� �������� � ȿ������ Ȯ�� ����</para>
/// <para>�䱸���� 2. ����� ������ �� UnitSelectBattleState�� ����</para>
/// <para>UI�� Ű�� ����, Ư�� ��ư�� ������ �����·� �����ϴ� State</para>
/// </summary>
public class ShowUnitDetailBattleState : BattleState
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
        owner.unitdetailUIController.cancelEvent.AddListener(OnCancelButton);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.unitdetailUIController.cancelEvent.RemoveListener(OnCancelButton);
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
    //�䱸���� 2 ����
    private void OnCancelButton()
    {
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //�䱸���� 1 ����
        owner.unitdetailUIController.Display(owner.selectedTarget);
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
