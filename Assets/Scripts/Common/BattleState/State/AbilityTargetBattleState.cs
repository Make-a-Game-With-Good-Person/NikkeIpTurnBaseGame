using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Linq;

/// <summary>
/// ��ų�� ������ Ȯ���ϰ� ��ų�� �����ϴ� ����
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
    TargetFinder TF;
    Vector2Int targetPos;
    LayerMask completeLayerMask;
    int MAX_COUNT = 2;
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

    bool RayRecursive(Vector3 origin, Vector3 dir, int count, Unit target)
    {
        if (count > MAX_COUNT)
            return false; // �ִ� ���̿� ���������Ƿ� ���� ó��

        if (Physics.Raycast(origin, dir, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == target.gameObject)
            {
                // Ÿ�� ������ ����ٸ� ����
                return true;
            }

            if (hit.transform.gameObject.layer == completeLayerMask)
            {
                // ���� ������ �¾��� ��, ���ο� ��������� ��� ȣ��
                Vector3 newOrigin = hit.point + dir * 1f; // �ణ �������� �̵�
                return RayRecursive(newOrigin, dir, count + 1, target);
            }
        }

        // Ÿ���� ������ ���� ���
        return false;
    }
    //�䱸���� 1
    private void ShowTargetableTiles() // ��ų ������ ��ư�� Ŭ���ϸ� ȣ���� �Լ�
    {
        owner.skillIdx = EventSystem.current.currentSelectedGameObject.GetComponent<UnitSkillButton>().skillIndexNumber; // �ٷ� ������ Ŭ���� ��ư�� �ҷ���
        owner.curSelectedSkill = owner.unitSkillManager.skillList[owner.skillIdx];
        owner.selectedSkillRangeTile = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, (from, to) => 
        { return from.distance + 1 <= owner.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= owner.curSelectedSkill.skillHeight; }
        );
        // selectedSkillRangeTile << hash set 
        // �� �ؽ����� Ű�� tile value�� dictionary���� ã�� �ش� Ÿ�Ͽ� �ִ� ������Ʈ �� ���鸸 ã��.
        // �� ���鿡�� ���̸� �� ����
        // ���̿� ��Ʈ�Ѱ� ���� ã������ ���̸� Ÿ�� ����, �ƴ϶�� �Ұ����ϴٰ� �Ǵ��� �ؽ��¿��� �ε��� ����

        HashSet<Vector2Int> temp = owner.selectedSkillRangeTile.ToHashSet();
        RaycastHit hit;

        /*foreach (Unit unit in owner.Units)
        {
            targetPos = owner.tileManager.GetTile(unit.transform.position).coordinate;
            if (!temp.Contains(targetPos)) continue;
            if(unit.gameObject.layer == 8)
            {
                Vector3 dir = unit.rayEnter.transform.position - owner.curControlUnit.rayPointer.transform.position;
                dir.Normalize();
                if (Physics.Raycast(owner.curControlUnit.rayPointer.position, dir, out hit, Mathf.Infinity))
                {
                    if(hit.transform.gameObject != unit.gameObject)
                    {
                        Debug.Log("������ ������ �ƴ�");
                        temp.Remove(targetPos);
                        continue;
                    }
                }
            }
        }*/

        /*foreach (Unit unit in owner.Units)
        {
            targetPos = owner.tileManager.GetTile(unit.transform.position).coordinate;
            if (!temp.Contains(targetPos)) continue;

            if (unit.gameObject.layer == 8)
            {
                Vector3 dir = unit.rayEnter.transform.position - owner.curControlUnit.rayPointer.transform.position;
                dir.Normalize();

                if (RayRecursive(owner.curControlUnit.rayPointer.position, dir, 0, unit))
                {
                    Debug.Log("Ÿ�� ������ ���������� ����");
                }
                else
                {
                    Debug.Log("Ÿ���� ����, ����Ʈ���� ����");
                    temp.Remove(targetPos);
                }
            }
        }*/

        TF.FindTarget(temp);
        owner.selectedSkillRangeTile = temp;
        owner.tileManager.ShowTiles(owner.selectedSkillRangeTile);
    }
    #endregion
    #region Protected

    protected override void Start()
    {
        base.Start();
        if(TF == null)
        {
            TF = new TargetFinder(owner);
        }
    }
    protected override void AddListeners()
    {
        base.AddListeners();
        owner.abilityTargetUIController.cancelButton.onClick.AddListener(OnCancelButton);
        owner.abilityTargetUIController.confirmButton.onClick.AddListener(OnConfirmButton);

        int index = owner.curControlUnit.index % 10;

        for(int i = 0; i < owner.abilityTargetUIController.skillButtonList[index].buttonList.Count; i++)
        {
            owner.abilityTargetUIController.skillButtonList[index].buttonList[i].onClick.AddListener(ShowTargetableTiles);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.abilityTargetUIController.cancelButton.onClick.RemoveListener(OnCancelButton);
        owner.abilityTargetUIController.confirmButton.onClick.RemoveListener(OnConfirmButton);
        int index = 0;
        if (owner.curControlUnit != null) index = owner.curControlUnit.index % 10;

        for (int i = 0; i < owner.abilityTargetUIController.skillButtonList[index].buttonList.Count; i++)
        {
            owner.abilityTargetUIController.skillButtonList[index].buttonList[i].onClick.RemoveListener(ShowTargetableTiles);
        }
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.curState = BATTLESTATE.ABILITYTARGET;
        owner.abilityTargetUIController.Display();
        owner.abilityTargetUIController.UISetting(owner.curControlUnit);
        completeLayerMask = 10;
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        owner.abilityTargetUIController.ResetSkillUI(owner.curControlUnit);
        owner.abilityTargetUIController.Hide();
    }
    #endregion
    #endregion

    #region EventHandlers
    //�䱸���� 2
    private void OnConfirmButton()
    {
        //owner.abilitytargets = ���õ�����.���õȴɷ�.�ɷ¹������() ������ ��� ��ȯ
        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
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
        /*if(owner.enemyTurn)
         * owner.target = owner.returnDecision.target;
         * owner.stateMachine.ChangeState<ConfirmAbilityBattleState>();
         */
        //ShowTargetableTiles();
        // ���⼭ curControlUnit�� index�� �����ؼ� ������ ��ų �������� Ȱ��ȭ �Ѵ�.
        // �� �� 
        // curControlUnit�� index�� �����ؼ� �� ��ų���� ������ image sprite�� �ȿ� ������� �����Ѵ�.

        //Ȯ�ΰ� �ڷ� ��ư UI Ȱ��ȭ
    }
    #endregion
    void OnDrawGizmos()
    {
        if (owner != null && owner.Units != null)
        {
            foreach (Unit unit in owner.Units)
            {
                if (unit.gameObject.layer == 8)
                {
                    Vector3 start = owner.curControlUnit.rayPointer.position;
                    Vector3 end = unit.rayEnter.transform.position;

                    // ���� ���� ���
                    Vector3 dir = end - start;
                    dir.Normalize();

                    // Gizmos ���� ����
                    Gizmos.color = Color.red;

                    // ���� ���� �׸���
                    Gizmos.DrawLine(start, start + dir * 100f);
                }
            }
        }
    }

    #region MonoBehaviour
    #endregion
}
