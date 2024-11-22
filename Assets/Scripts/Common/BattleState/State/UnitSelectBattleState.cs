using System.Collections;
using System.Collections.Generic;
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
        // ���⼭ owner.curControlUnit�� �ϳ��� ���� �س�����
        owner.curState = BATTLESTATE.UNITSELLECT;
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    //�䱸���� 1�� ����
    private void OnDrag()
    {
        //�� �ѷ�������
        // ����� ī�޶� ���� ��Ʈ�ѷ��� �� ����� ������ �� �ֵ��� ¥����. ���� ������ �������� �� ���� �ڵ尡 �ִٸ� �װɷ� ���� ����
    }
    // �䱸���� 3��, 4�� ����
    private void OnSelectUnit()
    {
        //if(������ �÷��̾� ��)
        //  owner.���� �����ϴ� ���� �ε��� = ������ ����
        //else ������ ������
        //  owner.���� ������ ���� = ������ ����
        //  owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
#if UNITY_EDITOR
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
        //owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //�䱸���� 7�� ����
    private void OnMoveButton()
    {
        //owner.stateMachine.ChangeState<MoveTargetBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //�䱸���� 10�� ����
        //bool playerturn = owner.CheckTurn();
        bool playerturn = true;

        if (playerturn)
        {
            //�䱸���� 2�� ���⿡ ����
            //�䱸���� 5�� ���⿡ ����
        }
        else
        {
            //��ǻ���� AI�� ȣ���ؼ� ����� ��
            //ChangeState�� ���⼭ ȣ��
        }
    }
    #endregion

    #region MonoBehaviour
    private void Update()
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
    }
    #endregion
}
