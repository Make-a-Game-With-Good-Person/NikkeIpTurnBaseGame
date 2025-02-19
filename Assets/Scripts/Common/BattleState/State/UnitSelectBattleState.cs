using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
    /*
     * 11-22 ���� ��Ȳ
     * �䱸���� 1 << �Ϸ�
     * �䱸���� 2,3,4 ������ ���� �߰��ϸ� �ٷ� ��
     * �䱸���� 6,7�� ��ư UI�� ������ �ٷ� ����
     * �䱸���� 5,8,9,10 �� ���� �ؾ���
     */
    #region Properties
    #region Private
    private Coroutine processing;
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
        if (owner.enemyTurn) return;
        owner.abilityMenuUIController.moveButton.onClick.AddListener(OnMoveButton);
        owner.abilityMenuUIController.abilityButton.onClick.AddListener(OnAbilityButton);
        owner.abilityMenuUIController.turnEndButton.onClick.AddListener(OnTurnEndButton);
        owner.abilityMenuUIController.vestiPassiveButton.onClick.AddListener(OnPassiveButton);

        foreach (Unit unit in owner.Units)
        {
            unit.GetComponent<UnitCamSetting>().unitSelectCamEvent.AddListener(OnSelectUnit);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.enemyTurn) return;
        owner.abilityMenuUIController.moveButton.onClick.RemoveListener(OnMoveButton);
        owner.abilityMenuUIController.abilityButton.onClick.RemoveListener(OnAbilityButton);
        owner.abilityMenuUIController.turnEndButton.onClick.RemoveListener(OnTurnEndButton);
        owner.abilityMenuUIController.vestiPassiveButton.onClick.RemoveListener(OnPassiveButton);

        foreach (Unit unit in owner.Units)
        {
            if (unit != null) unit.GetComponent<UnitCamSetting>().unitSelectCamEvent.RemoveListener(OnSelectUnit);
        }
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        // ���⼭ owner.curControlUnit�� �ϳ��� ���� �س�����
        owner.curState = BATTLESTATE.UNITSELLECT;
        Debug.Log("���� ����Ʈ ��Ʋ");
        //if(!owner.enemyturn)
        SelectFirstTarget();
        //owner.abilityMenuUIController.Display();

        if (processing != null)
        {
            StopCoroutine(processing);
        }
        processing = StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        //if(owner.enemyturn)
        base.Exit();
        owner.abilityMenuUIController.Hide();
    }
    #endregion
    #endregion

    #region EventHandlers
    //�� ���¿� ������ �� ���� ������ ��� �ִ� ����Ʈ �� ���� ���� �� �÷��̾�� ĳ���͸� ù ���� Ÿ������ ����
    private void SelectFirstTarget()
    {
        owner.abilityMenuUIController.Hide();

        if (owner.curControlUnit == null) // ������ ������ �ƹ��͵� ������, �����̰ų� ���� �ٲ��ų� �� �� �ϳ�
        {
            SelectPlayableUnit(owner.enemyTurn);

            if (owner.curControlUnit == null)
            {
                Debug.Log("���õ� ������ ����");
                owner.stateMachine.ChangeState<TurnCheckBattleState>();
                return;
            }

            if (!owner.enemyTurn)
            {
                owner.abilityMenuUIController.Display();
                owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);
            }
        }
        else // ������ ������ ���� ��
        {
            if (!owner.enemyTurn) // �÷��̾� ���� ��
            {
                owner.abilityMenuUIController.Display();
                owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);
            }
        }

        owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);
        Debug.Log($"���� ��Ʈ�� ���� ������ {owner.curControlUnit}");
    }
    void SelectPlayableUnit(bool EnemyTurn)
    {
        List<Unit> unitList = null;
        if (EnemyTurn)
        {
            unitList = owner.EnemyUnits;
        }
        else
        {
            unitList = owner.Units;
        }

        foreach (Unit unit in unitList)
        {
            if (unit.attackable || unit.movable)
            {
                owner.curControlUnit = unit;
                Debug.Log(unit.gameObject.name);
                owner.cameraStateController.SwitchToQuaterView(unit.transform);
                break;
            }
        }
    }

    // �䱸���� 3��, 4�� ����
    private void OnSelectUnit()
    {
        //if(������ �÷��̾� ��)
        //  owner.���� �����ϴ� ���� �ε��� = ������ ����
        //else ������ ������
        //  owner.���� ������ ���� = ������ ����
        //  owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
        Debug.Log("�¼���Ʈ����");
#if UNITY_EDITOR
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & owner.cameraStateController.layerMask) != 0) // Ŭ���� ����� ������ ��
            {
                // �� �ȿ��� �ٽ� �з�, �÷��̾ �����ϸ� �Ʒ� �ڵ�, �ƴϸ� ���� Ŭ������ �� �ڵ带 �ٽ� �ۼ�
                owner.cameraStateController.isDragging = false;
                //owner.cameraStateController.SetCamTarget(hit.collider.transform); // ���� Ŭ������ ���� ī�޶� ���ͺ�� �ش� ���� ���� �� �ִµ� �ڷΰ��� ��ư�� ������ �ٽ� curControlUnit�� Ÿ������ ������ ��
                owner.cameraStateController.SwitchToQuaterView(hit.collider.transform);

                Unit hitUnit = hit.collider.transform.GetComponent<Unit>();
                owner.selectedTarget = hitUnit.gameObject;

                // �� �Ʒ��� �з�, �±׷� �Ѵٰ� ġ��?
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    owner.curControlUnit = hitUnit; // �� curControlUnit�� ���ʿ� �� ���¿� ���� �� ���� �ϳ��� ���� �Ǿ��־����
                    owner.abilityMenuUIController.Display();
                    owner.abilityMenuUIController.ActivateButtons(hitUnit.attackable, hitUnit.movable);
                }
            }
            else // Ŭ���� ����� ������ �ƴ� ���� ��? UI�� Ŭ������ ���� ��� �غ����ҵ�.. < �׶��� �� LayerMask�� �ϴ��� �� ��� �����غ�
            {
                owner.abilityMenuUIController.Hide();
                owner.cameraStateController.SwitchToMapView();
            }
        }
#elif UNITY_ANDROID
        Touch touch = Input.GetTouch(0); // ù ��° ��ġ�� ��� (��Ƽ��ġ�� �ʿ� ���ٸ�)

        // ��ġ�� Ended �������� Ȯ�� (��ġ�� ������ �� ����)
        if (touch.phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position); // ��ġ ��ġ�� �̿��� Ray ����
            RaycastHit hit;
            if (((1 << hit.collider.gameObject.layer) & owner.cameraStateController.layerMask) != 0) // Ŭ���� ����� ������ ��
            {
                // �� �ȿ��� �ٽ� �з�, �÷��̾ �����ϸ� �Ʒ� �ڵ�, �ƴϸ� ���� Ŭ������ �� �ڵ带 �ٽ� �ۼ�
                owner.cameraStateController.isDragging = false;
                //owner.cameraStateController.SetCamTarget(hit.collider.transform); // ���� Ŭ������ ���� ī�޶� ���ͺ�� �ش� ���� ���� �� �ִµ� �ڷΰ��� ��ư�� ������ �ٽ� curControlUnit�� Ÿ������ ������ ��
                owner.cameraStateController.SwitchToQuaterView(hit.collider.transform);

                Unit hitUnit = hit.collider.transform.GetComponent<Unit>();
                owner.selectedTarget = hitUnit.gameObject;

                // �� �Ʒ��� �з�, �±׷� �Ѵٰ� ġ��?
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    owner.curControlUnit = hitUnit; // �� curControlUnit�� ���ʿ� �� ���¿� ���� �� ���� �ϳ��� ���� �Ǿ��־����
                    owner.abilityMenuUIController.Display();
                    owner.abilityMenuUIController.ActivateButtons(hitUnit.attackable, hitUnit.movable);
                }
            }
            else // Ŭ���� ����� ������ �ƴ� ���� ��? UI�� Ŭ������ ���� ��� �غ����ҵ�.. < �׶��� �� LayerMask�� �ϴ��� �� ��� �����غ�
            {
                owner.abilityMenuUIController.Hide();
                owner.cameraStateController.SwitchToMapView();
            }
        }
#endif
    }
    //�䱸���� 6�� ����
    private void OnAbilityButton()
    {
        owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //�䱸���� 7�� ����
    private void OnMoveButton()
    {
        owner.stateMachine.ChangeState<MoveTargetBattleState>();
    }

    void OnTurnEndButton()
    {
        owner.curControlUnit.attackable = false;
        owner.curControlUnit.movable = false;
        owner.curControlUnit = null;

        SelectFirstTarget();
    }

    void OnPassiveButton()
    {
        if (!owner.curControlUnit.attackable || owner.curControlUnit.movable) return;

        owner.curControlUnit.attackable = false;
        owner.curControlUnit.movable = true;

        owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);
    }

    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        if (!owner.enemyTurn)
        {
            if(owner.curControlUnit != null)
            {
                if (owner.curControlUnit.unitName != UnitName.Vesti)
                {
                    owner.abilityMenuUIController.TurnOffPassiveBT();
                }
                else
                {
                    owner.abilityMenuUIController.TurnOnPassiveBT();
                }
            }
        }
        yield return null;
        // �ӽ�, ���� ���� ����Ʈ�� �ѷ����� �� �ൿ�� ������ ������ ���� ���õ� ������ �Ѱ��� ���ٸ� ��üũ�� ������ ����Ʈ�� ��˻��غ�����
        if (owner.curControlUnit == null)
        {
            owner.stateMachine.ChangeState<TurnCheckBattleState>();
            yield break;
        }

        //�䱸���� 10�� ����
        if (!owner.enemyTurn)
        {
            
        }
        else
        {
            owner.curReturnDecision = owner.decisionTreeManager.RunAI(owner.curControlUnit.unitType);

            yield return new WaitForSeconds(1.5f);
            //��ǻ���� AI�� ȣ���ؼ� ����� ��
            if (owner.curControlUnit.unitType == UnitType.Enemy)
            {
                //owner.curReturnDecision = owner.curControlUnit.GetComponent<UnitDecisionTree>().Run();
                switch (owner.curReturnDecision.type)
                {
                    case ReturnDecision.DecisionType.Action:
                        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
                        break;
                    case ReturnDecision.DecisionType.Pass:
                        owner.curControlUnit.attackable = false;
                        owner.curControlUnit.movable = false;
                        owner.curControlUnit = null;
                        owner.stateMachine.ChangeState<TurnCheckBattleState>();
                        break;
                    case ReturnDecision.DecisionType.Move:
                        owner.stateMachine.ChangeState<MoveTargetBattleState>();
                        break;
                    case ReturnDecision.DecisionType.Alert:
                        owner.curControlUnit.transform.rotation = Quaternion.Euler(0, owner.curControlUnit.transform.eulerAngles.y + 90, 0);
                        owner.curControlUnit.attackable = false;
                        owner.curControlUnit.movable = false;
                        owner.curControlUnit = null;
                        owner.stateMachine.ChangeState<TurnCheckBattleState>();
                        break;
                }
            }
            else if(owner.curControlUnit.unitType == UnitType.Boss)
            {
                //owner.curReturnDecision = owner.curControlUnit.GetComponent<BossDecisionTree>().Run();

                switch (owner.curReturnDecision.type)
                {
                    case ReturnDecision.DecisionType.FarTargetAttack: // attackable�� false�� turnTwice�� ���� ����
                    case ReturnDecision.DecisionType.NearTargetAttack:
                        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();// attackable�� false�� turnTwice�� ���� ����
                        break;
                    case ReturnDecision.DecisionType.Summon:
                        owner.curControlUnit.GetComponent<Boss>().turnTwice = false;
                        owner.stateMachine.ChangeState<BossSummonState>(); // turnTwice�� false�� �ٲ�
                        break;
                    case ReturnDecision.DecisionType.AttackPass:// turnTwice�� false�� �ٲ�
                        owner.curControlUnit.attackable = false;
                        owner.stateMachine.ChangeState<TurnCheckBattleState>();
                        break;
                    case ReturnDecision.DecisionType.Pass:// turnTwice�� false�� �ٲ�
                        owner.curControlUnit.attackable = false;
                        owner.curControlUnit.GetComponent<Boss>().turnTwice = false;
                        owner.curControlUnit = null;
                        owner.stateMachine.ChangeState<TurnCheckBattleState>();
                        break;

                }
            }
            
            
            //ChangeState�� ���⼭ ȣ��
        }
    }
    #endregion

    #region MonoBehaviour
    /* private void Update()
     {
         if (owner.curState == BATTLESTATE.UNITSELLECT)
         {
 #if UNITY_EDITOR
             if (Input.GetMouseButtonDown(0)) // ���콺 Ŭ��(��ġ) �̺�Ʈ
             {
                 OnSelectUnit();
             }
 #elif UNITY_ANDROID
             if (Input.touchCount > 0)
             {
                 OnSelectUnit();
             }
 #endif
         }
     }*/
    #endregion
}
