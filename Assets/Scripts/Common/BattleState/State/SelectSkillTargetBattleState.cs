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
    CoverDirFinder CDF;
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
#if UNITY_EDITOR
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & owner.abilityTargetMask) != 0) // Ŭ���� ����� ���� ������ ����� ��
            {
                Debug.Log("��ų Ÿ�� ���� <<");
                //Vector2Int coord = new Vector2Int(Mathf.FloorToInt(worldpos.x / tileSize.x), Mathf.FloorToInt(worldpos.z / tileSize.z));
                /*targetPos.x = Mathf.FloorToInt(hit.collider.gameObject.transform.position.x);
                targetPos.y = Mathf.FloorToInt(hit.collider.gameObject.transform.position.z);*/
                targetPos = owner.tileManager.GetTile(hit.collider.gameObject.transform.position).coordinate;

                //Debug.Log(targetPos.x + ", " + targetPos.y);

                if (owner.selectedSkillRangeTile.Contains(targetPos))
                {
                    Debug.Log("��ų Ÿ�� ���� <<");
                    owner.selectedTarget = hit.collider.gameObject;
                    Unit selectedUnit = owner.selectedTarget.GetComponent<Unit>();
                    CDF.SetFinder(owner.curControlUnit.gameObject, owner.selectedTarget, selectedUnit.tile.covers);

                    GameObject nearestCover = CDF.FindCover();
                    if (nearestCover != null)
                    {
                        owner.selectedTarget = nearestCover;
                    }

                    owner.stateMachine.ChangeState<ConfirmAbilityTargetBattleState>();
                }
            }
        }
#elif UNITY_ANDROID
        Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ��� (��Ƽ��ġ�� �ʿ� ���ٸ�)

        // ��ġ�� Ended �������� Ȯ�� (��ġ�� ������ �� ����)
        if (touch.phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position); // ��ġ ��ġ�� �̿��� Ray ����
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (((1 << hit.collider.gameObject.layer) & owner.abilityTargetMask) != 0) // Ŭ���� ����� ���� ������ ����� ��
                {
                    Debug.Log("��ų Ÿ�� ���� <<");
                    //Vector2Int coord = new Vector2Int(Mathf.FloorToInt(worldpos.x / tileSize.x), Mathf.FloorToInt(worldpos.z / tileSize.z));
                    /*targetPos.x = Mathf.FloorToInt(hit.collider.gameObject.transform.position.x);
                    targetPos.y = Mathf.FloorToInt(hit.collider.gameObject.transform.position.z);*/
                    targetPos = owner.tileManager.GetTile(hit.collider.gameObject.transform.position).coordinate;

                    //Debug.Log(targetPos.x + ", " + targetPos.y);

                    if (owner.selectedSkillRangeTile.Contains(targetPos))
                    {
                        Debug.Log("��ų Ÿ�� ���� <<");
                        owner.selectedTarget = hit.collider.gameObject;
                        Unit selectedUnit = owner.selectedTarget.GetComponent<Unit>();
                        CDF.SetFinder(owner.curControlUnit.gameObject, owner.selectedTarget, selectedUnit.tile.covers);

                        GameObject nearestCover = CDF.FindCover();
                        if (nearestCover != null)
                        {
                            owner.selectedTarget = nearestCover;
                        }

                        owner.stateMachine.ChangeState<ConfirmAbilityTargetBattleState>();
                    }
                }
            }
        }
#endif
    }

    void EnemySetTargetPlayer()
    {
        Unit selectedUnit = owner.selectedTarget.GetComponent<Unit>();
        CDF.SetFinder(owner.curControlUnit.gameObject, owner.selectedTarget, selectedUnit.tile.covers);

        GameObject nearestCover = CDF.FindCover();
        if (nearestCover != null)
        {
            owner.selectedTarget = nearestCover;
        }

        owner.stateMachine.ChangeState<ConfirmAbilityTargetBattleState>();
    }
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
        if (!owner.enemyTurn)
        {
            // ���� �����ϴ� ���鿡�� Ŭ�� ���� �� ��ġ�� �Ǻ��� owner�� selectedSkillRangeTile �ȿ� �� ��ġ�� ���� �Ǵ��� Ȯ�� �� ���� �Ѵٸ� Ÿ���� �����ϴ� �Լ��� addListener�Ѵ�.
            owner.selectSkillTargetUIController.cancelButton.onClick.AddListener(OnCancelButton);
            foreach (Unit unit in owner.EnemyUnits)
            {
                if (((1 << unit.gameObject.layer) & owner.abilityTargetMask) != 0)
                {
                    unit.GetComponent<AbilityTargetting>().abilityTargetAct.AddListener(SetAbilityTarget);
                }
            }
        }


    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        // ���⼱ ������
        if (!owner.enemyTurn)
        {
            owner.selectSkillTargetUIController.cancelButton.onClick.RemoveListener(OnCancelButton);
            foreach (Unit unit in owner.EnemyUnits)
            {
                if (unit == null) continue;

                if (((1 << unit.gameObject.layer) & owner.abilityTargetMask) != 0)
                {
                    if (unit != null) unit.GetComponent<AbilityTargetting>().abilityTargetAct.RemoveListener(SetAbilityTarget);
                }
            }
        }
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();

        if (CDF == null)
        {
            CDF = new CoverDirFinder();
        }

        owner.curState = BATTLESTATE.SELECTSKILLTARGET;

        if (!owner.enemyTurn) owner.selectSkillTargetUIController.Display();
        else
        {
            owner.selectSkillTargetUIController.Hide();
        }
        // ���� ������ ������ ��ų�� ������ ���������
        // ��ų ���� �ȿ� ���� �ִ°� �Ǻ��� ���� �ִٸ� ���� ��¦�̰�? �ٲ������..
        // �׸��� UI�� �Ѿ���, �ڷΰ��� ��ư�� ��������ϴϱ�
        // ��ư �Ӹ� �ƴ϶� �ؽ�Ʈ�� ����� �����ϼ��� �̷��͵� ���������
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        owner.selectSkillTargetUIController.Hide();
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
        if (owner.enemyTurn)
        {
            EnemySetTargetPlayer();
        }
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
