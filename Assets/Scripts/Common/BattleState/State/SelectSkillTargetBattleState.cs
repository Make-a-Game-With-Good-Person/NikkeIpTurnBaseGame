using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 전 상태에서 선택한 스킬을 쏠 타겟을 정하는 상태.
/// <para>요구사항 1. 공격가능한 대상 표시</para>
/// <para>요구사항 2. 공격 대상을 클릭하면 ConfirmAbilityTargetBattleState</para>
/// <para>요구사항 3. 뒤로가기 버튼을 눌렀을 때 AbilityTargetBattleState 복귀</para>
/// <para>요구사항 4. 공격 가능한 대상들을 파악해 몸을 빨갛게 점멸 시킨다.</para>
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
    //요구사항 1
    void SetAbilityTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (((1 << hit.collider.gameObject.layer) & owner.abilityTargetMask) != 0) // 클릭한 대상이 공격 가능한 대상일 때
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
        // 씬에 존재하는 적들에게 클릭 했을 시 위치를 판별해 owner의 selectedSkillRangeTile 안에 그 위치가 포함 되는지 확인 후 포함 한다면 타겟팅 설정하는 함수를 addListener한다.
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
        // 여기선 리무브
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
        // 내가 이전에 선택한 스킬의 범위를 보여줘야함
        // 스킬 범위 안에 적이 있는걸 판별해 적이 있다면 적도 반짝이게? 바꿔줘야함..
        // 그리고 UI도 켜야함, 뒤로가는 버튼은 보여줘야하니깐
        // 버튼 뿐만 아니라 텍스트로 대상을 선택하세요 이런것도 보여줘야함
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();

    }
    #endregion
    #endregion

    #region EventHandlers
    //요구사항 2
    private void OnConfirmButton()
    {
        //owner.abilitytargets = 선택된유닛.선택된능력.능력범위계산() 범위내 대상 반환
        //owner.stateMachine.ChangeState<ConfirmAbilityBattleState>();
    }
    //요구사항 3
    private void OnCancelButton()
    {
        //확인과 뒤로 버튼 UI 비활성화
        owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //요구사항 5
    private void OnChangeTargetCommand()
    {
        //선택한 능력이 단일대상일 경우에만 작동
        //왼쪽 오른쪽 화살표로 대상이 될 유닛을 선택하는 함수
        //선택된 유닛의 정보를 간단하게 바꿔줘야함
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        //ShowTargetableTiles();
        // 여기서 curControlUnit의 index를 참조해서 가능한 스킬 아이콘을 활성화 한다.
        // 할 일 
        // curControlUnit의 index를 참조해서 각 스킬들의 아이콘 image sprite랑 안에 스펙들을 조정한다.

        //확인과 뒤로 버튼 UI 활성화
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
