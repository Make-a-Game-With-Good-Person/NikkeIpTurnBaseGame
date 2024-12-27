using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        owner.abilityMenuUIController.turnReButton.onClick.AddListener(OnTurnReAgainButton);

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
        owner.abilityMenuUIController.turnReButton.onClick.RemoveListener(OnTurnReAgainButton);

        foreach (Unit unit in owner.Units)
        {
            if (unit != null) unit.GetComponent<UnitCamSetting>().unitSelectCamEvent.RemoveListener(OnSelectUnit);
        }
    }
    #endregion
    #region Public
    public override void Enter()
    {
        //if(owner.enemyturn)
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
        if (owner.enemyTurn)
        {
            foreach (Unit unit in owner.Units)
            {
                Debug.Log(unit.gameObject.name);
                if (unit.gameObject.CompareTag("Enemy") && (unit.attackable || unit.movable))
                {
                    owner.curControlUnit = unit;
                    owner.cameraStateController.SwitchToQuaterView(unit.transform);
                    /*owner.abilityMenuUIController.Display();
                    owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);*/
                    break;
                }
            }
        }


        if (owner.curControlUnit != null)
        {
            owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);
            /*owner.abilityMenuUIController.Display();
            owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);*/
        }
        else
        {
            foreach (Unit unit in owner.Units)
            {
                Debug.Log(unit.gameObject.name);
                if (unit.gameObject.CompareTag("Player") && (unit.attackable || unit.movable))
                {
                    owner.curControlUnit = unit;
                    owner.cameraStateController.SwitchToQuaterView(unit.transform);
                    /*owner.abilityMenuUIController.Display();
                    owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);*/
                    break;
                }
            }
        }

        if (owner.curControlUnit == null)
        {
            // �̶��� �� ���̴ϱ� ProcessingState ���⼭ ó���� �� �ֵ��� ��ġ�� ���ؾ��Ѵ�.
        }
        else
        {
            owner.abilityMenuUIController.Display();
            owner.abilityMenuUIController.ActivateButtons(owner.curControlUnit.attackable, owner.curControlUnit.movable);
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
                if (Physics.Raycast(ray, out hit))
                {
                    if (((1 << hit.collider.gameObject.layer) & owner.cameraStateController.layerMask) != 0) // Ŭ���� ����� ������ ��
                    {
                    // �� �ȿ��� �ٽ� �з�, �÷��̾ �����ϸ� �Ʒ� �ڵ�, �ƴϸ� ���� Ŭ������ �� �ڵ带 �ٽ� �ۼ�
                    owner.cameraStateController.isDragging = false;
                    owner.cameraStateController.SetCamTarget(hit.collider.transform); // ���� Ŭ������ ���� ī�޶� ���ͺ�� �ش� ���� ���� �� �ִµ� �ڷΰ��� ��ư�� ������ �ٽ� curControlUnit�� Ÿ������ ������ ��
                    owner.cameraStateController.SwitchToQuaterView();

                    Unit hitUnit = hit.collider.transform.GetComponent<Unit>();
                    // �� �Ʒ��� �з�, �±׷� �Ѵٰ� ġ��?
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        owner.selectedTarget = hitUnit;
                        //owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
                    }
                    else if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        owner.curControlUnit = hitUnit; // �� curControlUnit�� ���ʿ� �� ���¿� ���� �� ���� �ϳ��� ���� �Ǿ��־����
                    }
                }
                else // Ŭ���� ����� ������ �ƴ� ���� ��? UI�� Ŭ������ ���� ��� �غ����ҵ�.. < �׶��� �� LayerMask�� �ϴ��� �� ��� �����غ�
                {
                    owner.cameraStateController.SwitchToMapView();
                }
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

    void OnTurnReAgainButton()
    {
        owner.curControlUnit.attackable = owner.curControlUnit.attack_Re;
        owner.curControlUnit.movable = owner.curControlUnit.move_Re;

        SelectFirstTarget();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //

        //�䱸���� 10�� ����
        if (!owner.enemyTurn)
        {
            //�䱸���� 2�� ���⿡ ����
            //�䱸���� 5�� ���⿡ ����
            // ������ Ŭ���ϸ� ī�޶� �̺�Ʈ�� ������ �����ߴٰ� ó���ϱ� ������ ���⼱ �ʿ��� UI���� ���ָ� �ɰ� ����


        }
        else
        {
            //��ǻ���� AI�� ȣ���ؼ� ����� ��
            ReturnDecision returnDecision = owner.curControlUnit.GetComponent<UnitDecisionTree>().Run();
            switch (returnDecision.type)
            {
                case ReturnDecision.DecisionType.Action:
                    owner.curControlUnit.GetComponent<UnitDecisionTree>().returnDecision = returnDecision;
                    owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
                    break;
                    /*case ReturnDecision.DecisionType.Move:
                        owner.ReturnDecision = return;
                        owner.stateMachine.ChangeState<MoveTargetBattleState>();
                        break;
                    case ReturnDecision.DecisionType.Pass:
                        owner.curcontrollunit.movable = false;
                        owner.curcontrollunit.attackable = false;
                        owner.CheckTurn();
                        if (!onwer.enemyturn)
                        {
                            Enter();
                        }
                        break;*/
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
