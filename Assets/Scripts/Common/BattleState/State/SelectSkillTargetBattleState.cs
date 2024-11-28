using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    Vector2Int targetPos = new Vector2Int();
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
    void SetAbilityTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & owner.abilityTargetMask) != 0) // Ŭ���� ����� ���� ������ ����� ��
            {
                targetPos.x = (int)hit.collider.gameObject.transform.position.x;
                targetPos.y = (int)hit.collider.gameObject.transform.position.z;

                if (owner.selectedSkillRangeTile.Contains(targetPos))
                {
                    owner.selectedTarget = hit.collider.gameObject.GetComponent<Unit>();
                    owner.stateMachine.ChangeState<ConfirmAbilityTargetBattleState>();
                }
            }
        }
    }

    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        // ���� �����ϴ� ���鿡�� Ŭ�� ���� �� ��ġ�� �Ǻ��� owner�� selectedSkillRangeTile �ȿ� �� ��ġ�� ���� �Ǵ��� Ȯ�� �� ���� �Ѵٸ� Ÿ���� �����ϴ� �Լ��� addListener�Ѵ�.
        owner.selectSkillTargetUIController.cancelButton.onClick.AddListener(OnCancelButton);
        foreach (Unit unit in owner.Units)
        {
            if (((1 << unit.gameObject.layer) & owner.abilityTargetMask) != 0)
            {
                unit.GetComponent<AbilityTargetting>().abilityTargetAct.AddListener(SetAbilityTarget);
            }
        }

    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        // ���⼱ ������
        owner.selectSkillTargetUIController.cancelButton.onClick.RemoveListener(OnCancelButton);
        foreach (Unit unit in owner.Units)
        {
            if (((1 << unit.gameObject.layer) & owner.abilityTargetMask) != 0)
            {
                unit.GetComponent<AbilityTargetting>().abilityTargetAct.RemoveListener(SetAbilityTarget);
            }
        }

    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.curState = BATTLESTATE.SELECTSKILLTARGET;
        // ���� ������ ������ ��ų�� ������ ���������
        // ��ų ���� �ȿ� ���� �ִ°� �Ǻ��� ���� �ִٸ� ���� ��¦�̰�? �ٲ������..
        // �׸��� UI�� �Ѿ���, �ڷΰ��� ��ư�� ��������ϴϱ�
        // ��ư �Ӹ� �ƴ϶� �ؽ�Ʈ�� ����� �����ϼ��� �̷��͵� ���������
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
