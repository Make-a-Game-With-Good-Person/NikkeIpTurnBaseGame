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
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    //요구사항 1번 구현
    private void OnDrag()
    {
        //맵 둘러보게함
    }
    // 요구사항 3번, 4번 구현
    private void OnSelectUnit()
    {
        //if(유닛이 플레이어 것)
        //  owner.현재 조종하는 유닛 인덱스 = 선택한 유닛
        //else 유닛이 적유닛
        //  owner.현재 선택한 유닛 = 선택한 유닛
        //  owner.stateMachine.ChangeState<ShowUnitDetailBattleState>();
    }
    //요구사항 6번 구현
    private void OnAbilityButton()
    {
        //owner.stateMachine.ChangeState<AbilityTargetBattleState>();
    }
    //요구사항 7번 구현
    private void OnMoveButton()
    {
        //owner.stateMachine.ChangeState<MoveTargetBattleState>();
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
        }
        else 
        { 
            //컴퓨터의 AI를 호출해서 결과를 냄
            //ChangeState를 여기서 호출
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
