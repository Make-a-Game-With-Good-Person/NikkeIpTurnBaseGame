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
/// <para>�� ��ų �������� ������ ��ų ���� �����Ÿ��� Ÿ�� ���� �ٲٴ°ɷ� ǥ���ؼ� �˷���</para>
/// <para>��ų �������� ������ Ȯ���� ������ ���� ���·� ���� >> ���� ���¿��� ���� �����ϰ� ���� ���·� �Ѿ�� �׶� Confirm���� ����.</para>
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
    //�䱸���� 1
    private void ShowTargetableTiles()
    {
        //Unit selected = owner.units[owner.select];
        //HashSet<Vector2Int> movableTiles = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, ���õ������� �ɷ��� ��Ÿ� , ���õ� ������ �ɷ��� ��밡�� ����);
        //owner.tileManager.ShowTiles(movableTiles);
    }
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        owner.abilityTargetUIController.cancelButton.onClick.AddListener(OnCancelButton);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.abilityTargetUIController.cancelButton.onClick.RemoveListener(OnCancelButton);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.curState = BATTLESTATE.ABILITYTARGET;
        owner.abilityTargetUIController.Display();
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        owner.abilityTargetUIController.Hide();
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
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
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
