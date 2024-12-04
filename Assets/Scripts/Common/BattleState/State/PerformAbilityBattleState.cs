using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

/// <summary>
/// 선택한 어빌리티를 이제 진짜로 실행함
/// <para>요구사항 1. 시행하고 나서 GameEnd 체크(몹들이 전부 죽었는지 아군이 전부 죽었는지 등)</para>
/// <para>요구사항 2. 아직 아군 유닛을 전부 다 진행하지 않았으면 UnitSelectBattleState로 복귀</para>
/// <para>요구사항 3. 아군 유닛을 전부 진행했을 때는 owner의 변수를 변경할 것</para>
/// <para>요구사항 4. 이번 유닛의 Ability 버튼을 비활성화할 것</para>
/// </summary>
public class PerformAbilityBattleState : BattleState
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
        owner.curState = BATTLESTATE.PERFORMABILITY;
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);

        // 여기서 필드 위에 적들이 모두 죽었는지 체크 후 죽었다면 스테이지를 종료하면 됨

        owner.curControlUnit.attackable = false; // 공격은 끝났음
        owner.curControlUnit.attack_Re = false; // 임시도 닫아버림

        if (owner.curControlUnit.movable) // 아직 최근에 컨트롤한 유닛이 이동 가능하다면
        {
            owner.stateMachine.ChangeState<UnitSelectBattleState>(); // 바로 상태 넘기기
        }
        else // 만약 공격,이동을 모두 다 했다면
        {
            owner.curControlUnit = null;

            foreach (Unit unit in owner.Units)
            {
                if (unit.gameObject.layer == 7 && (unit.attackable || unit.movable))
                {
                    owner.curControlUnit = unit;
                }
            }

            if (owner.curControlUnit == null)
            {
                // 적의 턴으로 넘긴다고 생각해야함.
                // 처리할 변수를 만든 뒤 Selected나 어디로 넘겨야함
                Debug.Log("모든 아군이 턴을 다 소모했습니다.");


            }

            owner.stateMachine.ChangeState<UnitSelectBattleState>();
        }
        
        
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
