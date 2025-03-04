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
        owner.curState = BATTLESTATE.MOVESEQUENCE;
        owner.cameraStateController.SwitchToQuaterView(owner.curControlUnit.transform);
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
        
        if (owner.tile != owner.curControlUnit.tile)
        {
            owner.tileVFXManager.TurnOnTileTouch(owner.tile.center);
            //owner.curControlUnit.animator.SetBool("Move", true);
            yield return StartCoroutine(movement.Traverse(owner.tile.coordinate, owner.tileManager));
        }
        else
        {
            yield return null;
        }
        //owner.curControlUnit.animator.SetBool("Move", false);
        owner.curControlUnit.movable = false;
        owner.tileVFXManager.TurnOffTileTouch();
        owner.stateMachine.ChangeState<TurnCheckBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
