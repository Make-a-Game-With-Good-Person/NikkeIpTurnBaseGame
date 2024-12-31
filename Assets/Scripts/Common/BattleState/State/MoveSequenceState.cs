using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 실제로 이동하는 상태
/// <para>요구사항 1. 유닛들의 애니메이션 활성화</para>
/// <para>요구사항 2. 지나가면서 지뢰등이 활성화 되면 처리</para>
/// <para>요구사항 3. 경계 구역 같은데 지나가면 맞는것 처리</para>
/// <para>요구사항 4. 끝났을때 SelectUnitBattleState 복귀</para>
/// </summary>
public class MoveSequenceState : BattleState
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
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        UnitMovement movement = owner.curControlUnit.GetComponent<UnitMovement>();
           
        if(owner.tile != owner.curControlUnit.tile)
        {
            yield return StartCoroutine(movement.Traverse(owner.tile.coordinate, owner.tileManager));
        }

        owner.curControlUnit.movable = false;
        owner.curControlUnit.move_Re = false; // 임시도 닫아버림

        if (owner.enemyTurn)
        {
            owner.stateMachine.ChangeState<UnitSelectBattleState>(); // 바로 상태 넘기기
            yield break;
        }

        if (owner.curControlUnit.attackable)
        {
            owner.stateMachine.ChangeState<UnitSelectBattleState>();
        }
        else
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
                owner.enemyTurn = true;

            }

            owner.stateMachine.ChangeState<UnitSelectBattleState>();
        }
        
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
