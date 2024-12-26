using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using System.Linq;

/// <summary>
/// 스킬의 스펙을 확인하고 스킬을 선택하는 상태
/// <para>요구사항 1. 공격가능한 타일 표시</para>
/// <para>요구사항 2. 공격 대상이 있을 때 선택 버튼을 누르면 ConfirmAbilityTargetBattleState로 진행</para>
/// <para>요구사항 3. 뒤로가기 버튼을 눌렀을 때 UnitSelectBattleState로 복귀</para>
/// <para>요구사항 4. 공격 대상이 될만한 유닛이나 물건들을 빨간색으로든 뭐든 표시해줌</para>
/// <para>요구사항 5. 공격이 단일 대상 공격이면 옆 버튼으로 공격할 유닛 선택 가능</para>
/// <para>각 스킬 아이콘을 누르면 스킬 별로 사정거리를 타일 색을 바꾸는걸로 표시해서 알려줌</para>
/// <para>스킬 아이콘을 누르고 확인을 누르면 다음 상태로 이전 >> 다음 상태에선 적을 선택하고 다음 상태로 넘어가면 그때 Confirm으로 간다.</para>
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
            return false; // 최대 깊이에 도달했으므로 실패 처리

        if (Physics.Raycast(origin, dir, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject == target.gameObject)
            {
                // 타겟 유닛을 맞췄다면 성공
                return true;
            }

            if (hit.transform.gameObject.layer == completeLayerMask)
            {
                // 완전 엄폐물을 맞았을 때, 새로운 출발점으로 재귀 호출
                Vector3 newOrigin = hit.point + dir * 1f; // 약간 앞쪽으로 이동
                return RayRecursive(newOrigin, dir, count + 1, target);
            }
        }

        // 타겟을 맞추지 못한 경우
        return false;
    }
    //요구사항 1
    private void ShowTargetableTiles() // 스킬 아이콘 버튼을 클릭하면 호출할 함수
    {
        owner.skillIdx = EventSystem.current.currentSelectedGameObject.GetComponent<UnitSkillButton>().skillIndexNumber; // 바로 직전에 클릭한 버튼을 불러옴
        owner.curSelectedSkill = owner.unitSkillManager.skillList[owner.skillIdx];
        owner.selectedSkillRangeTile = owner.tileManager.SearchTile(owner.curControlUnit.tile.coordinate, (from, to) => 
        { return from.distance + 1 <= owner.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= owner.curSelectedSkill.skillHeight; }
        );
        // selectedSkillRangeTile << hash set 
        // 이 해쉬셋을 키로 tile value를 dictionary에서 찾아 해당 타일에 있는 오브젝트 중 적들만 찾음.
        // 그 적들에게 레이를 다 쏴서
        // 레이에 히트한게 내가 찾고자한 적이면 타격 가능, 아니라면 불가능하다고 판단해 해쉬셋에서 인덱스 삭제

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
                        Debug.Log("맞은게 유닛이 아님");
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
                    Debug.Log("타겟 유닛을 성공적으로 맞춤");
                }
                else
                {
                    Debug.Log("타겟팅 실패, 리스트에서 제거");
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
    //요구사항 2
    private void OnConfirmButton()
    {
        //owner.abilitytargets = 선택된유닛.선택된능력.능력범위계산() 범위내 대상 반환
        owner.stateMachine.ChangeState<SelectSkillTargetBattleState>();
    }
    //요구사항 3
    private void OnCancelButton()
    {
        //확인과 뒤로 버튼 UI 비활성화
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
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
        /*if(owner.enemyTurn)
         * owner.target = owner.returnDecision.target;
         * owner.stateMachine.ChangeState<ConfirmAbilityBattleState>();
         */
        //ShowTargetableTiles();
        // 여기서 curControlUnit의 index를 참조해서 가능한 스킬 아이콘을 활성화 한다.
        // 할 일 
        // curControlUnit의 index를 참조해서 각 스킬들의 아이콘 image sprite랑 안에 스펙들을 조정한다.

        //확인과 뒤로 버튼 UI 활성화
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

                    // 레이 방향 계산
                    Vector3 dir = end - start;
                    dir.Normalize();

                    // Gizmos 색상 설정
                    Gizmos.color = Color.red;

                    // 레이 라인 그리기
                    Gizmos.DrawLine(start, start + dir * 100f);
                }
            }
        }
    }

    #region MonoBehaviour
    #endregion
}
