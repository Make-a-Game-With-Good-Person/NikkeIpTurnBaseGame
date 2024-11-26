using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 시작하고 유닛 선택하는 상태
/// <para>요구사항 1. 터치로 끌어서 맵을 둘러볼 수 있음</para>
/// <para>요구사항 2. 플레이어 차례일때 기본적으로 하나의 유닛을 선택한 상태로 있음</para>
/// <para>요구사항 3. 플레이어 차례일때 다른 유닛을 터치하면 그 유닛을 조종하는 것으로 넘어감</para>
/// <para>요구사항 4. 적군 유닛을 선택하면 ShowUnitDetailBattleState로 진행</para>
/// <para>요구사항 5. 밑에는 할 수 있는 행동이 표시됨</para>
/// <para>요구사항 6. 공격 버튼을 누를 시, AbilityTargetBattleState로 진행</para>
/// <para>요구사항 7. 이동 버튼을 누를 시, MoveTargetBattleState로 진행</para>
/// <para>요구사항 8. 유닛을 꾹 누르거나 상세정보 버튼을 누르면, ShowUnitDetailBattleState로 진행</para>
/// <para>요구사항 9. 대기 버튼을 누를 시, 해당 유닛의 턴을 종료, 아직 턴이 남아있는 유닛중 하나로 자동이동</para>
/// <para>요구사항 10. 마지막 유닛까지 끝났을때, 상대편의 턴으로</para>
/// </summary>
public class UnitSelectBattleState : BattleState
{
    /*
     * 11-22 진행 상황
     * 요구사항 1 << 완료
     * 요구사항 2,3,4 까지는 몇줄 추가하면 바로 완
     * 요구사항 6,7은 버튼 UI만 있으면 바로 가능
     * 요구사항 5,8,9,10 은 개발 해야함
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
        owner.abilityMenuUIController.moveButton.onClick.AddListener(OnMoveButton);
        owner.abilityMenuUIController.abilityButton.onClick.AddListener(OnAbilityButton);

        foreach (Unit unit in owner.Units)
        {
            unit.GetComponent<UnitCamSetting>().unitSelectCamEvent.AddListener(OnSelectUnit);
        }
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.abilityMenuUIController.moveButton.onClick.RemoveListener(OnMoveButton);
        owner.abilityMenuUIController.abilityButton.onClick.RemoveListener(OnAbilityButton);

        foreach (Unit unit in owner.Units)
        {
            unit.GetComponent<UnitCamSetting>().unitSelectCamEvent.RemoveListener(OnSelectUnit);
        }
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        // 여기서 owner.curControlUnit을 하나는 결정 해놔야함
        owner.curState = BATTLESTATE.UNITSELLECT;
        Debug.Log("유닛 셀렉트 배틀");
        SelectFirstTarget();
        //owner.abilityMenuUIController.Display();

        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
        owner.abilityMenuUIController.Hide();
    }
    #endregion
    #endregion

    #region EventHandlers
    //이 상태에 들어왔을 때 현재 유닛을 담고 있는 리스트 중 가장 먼저 들어간 플레이어블 캐릭터를 첫 제어 타겟으로 결정
    private void SelectFirstTarget()
    {
        if(owner.curControlUnit != null)
        {
            Debug.Log("커런트 있음");
            owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);
            owner.abilityMenuUIController.Display();
        }
        else
        {
            foreach (Unit unit in owner.Units)
            {
                Debug.Log(unit.gameObject.name);
                if (unit.gameObject.CompareTag("Player"))
                {
                    owner.curControlUnit = unit;
                    owner.cameraStateController.SwitchToQuaterView(unit.transform);
                    break;
                }
            }
        }

        

    }
    // 요구사항 3번, 4번 구현
    private void OnSelectUnit()
    {
        //if(유닛이 플레이어 것)
        //  owner.현재 조종하는 유닛 인덱스 = 선택한 유닛
        //else 유닛이 적유닛
        //  owner.현재 선택한 유닛 = 선택한 유닛
        //  owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
        Debug.Log("온셀렉트유닛");
#if UNITY_EDITOR
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & owner.cameraStateController.layerMask) != 0) // 클릭한 대상이 유닛일 때
            {
                // 이 안에서 다시 분류, 플레이어를 선택하면 아래 코드, 아니면 적을 클릭했을 때 코드를 다시 작성
                owner.cameraStateController.isDragging = false;
                //owner.cameraStateController.SetCamTarget(hit.collider.transform); // 적을 클릭했을 때도 카메라가 쿼터뷰로 해당 적을 잡을 수 있는데 뒤로가기 버튼을 누르면 다시 curControlUnit을 타겟으로 잡으면 됨
                owner.cameraStateController.SwitchToQuaterView(hit.collider.transform);

                Unit hitUnit = hit.collider.transform.GetComponent<Unit>();
                owner.selectedTarget = hitUnit;

                // 이 아래로 분류, 태그로 한다고 치면?
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    owner.curControlUnit = hitUnit; // 이 curControlUnit은 최초에 이 상태에 들어올 때 누구 하나는 결정 되어있어야함
                    owner.abilityMenuUIController.Display();
                }
            }
            else // 클릭한 대상이 유닛이 아닌 맵일 때? UI를 클릭했을 때도 고려 해봐야할듯.. < 그때는 또 LayerMask로 하던가 뭐 어떻게 조절해봄
            {
                Debug.Log("맵뷰 ㄱㄱ");
                owner.abilityMenuUIController.Hide();
                owner.cameraStateController.SwitchToMapView();
            }
        }
#elif UNITY_ANDROID
        Touch touch = Input.GetTouch(0); // 첫 번째 터치만 사용 (멀티터치가 필요 없다면)

        // 터치가 Ended 상태인지 확인 (터치가 끝났을 때 동작)
        if (touch.phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position); // 터치 위치를 이용해 Ray 생성
            RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (((1 << hit.collider.gameObject.layer) & owner.cameraStateController.layerMask) != 0) // 클릭한 대상이 유닛일 때
                    {
                    // 이 안에서 다시 분류, 플레이어를 선택하면 아래 코드, 아니면 적을 클릭했을 때 코드를 다시 작성
                    owner.cameraStateController.isDragging = false;
                    owner.cameraStateController.SetCamTarget(hit.collider.transform); // 적을 클릭했을 때도 카메라가 쿼터뷰로 해당 적을 잡을 수 있는데 뒤로가기 버튼을 누르면 다시 curControlUnit을 타겟으로 잡으면 됨
                    owner.cameraStateController.SwitchToQuaterView();

                    Unit hitUnit = hit.collider.transform.GetComponent<Unit>();
                    // 이 아래로 분류, 태그로 한다고 치면?
                    if (hit.collider.gameObject.CompareTag("Enemy"))
                    {
                        owner.selectedTarget = hitUnit;
                        //owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
                    }
                    else if (hit.collider.gameObject.CompareTag("Player"))
                    {
                        owner.curControlUnit = hitUnit; // 이 curControlUnit은 최초에 이 상태에 들어올 때 누구 하나는 결정 되어있어야함
                    }
                }
                else // 클릭한 대상이 유닛이 아닌 맵일 때? UI를 클릭했을 때도 고려 해봐야할듯.. < 그때는 또 LayerMask로 하던가 뭐 어떻게 조절해봄
                {
                    owner.cameraStateController.SwitchToMapView();
                }
            }
        }
#endif
    }
    //요구사항 6번 구현
    private void OnAbilityButton()
    {
        owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //요구사항 7번 구현
    private void OnMoveButton()
    {
        owner.stateMachine.ChangeState<MoveTargetBattleState>();
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //요구사항 10번 구현
        //bool playerturn = owner.CheckTurn();
        bool playerturn = true;

        if (playerturn)
        {
            //요구사항 2번 여기에 구현
            //요구사항 5번 여기에 구현
            // 유닛을 클릭하면 카메라 이벤트를 실행해 선택했다고 처리하기 때문에 여기선 필요한 UI들을 켜주면 될거 같음
            

        }
        else
        {
            //컴퓨터의 AI를 호출해서 결과를 냄
            //ChangeState를 여기서 호출
        }
    }
    #endregion

    #region MonoBehaviour
   /* private void Update()
    {
        if (owner.curState == BATTLESTATE.UNITSELLECT)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0)) // 마우스 클릭(터치) 이벤트
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
