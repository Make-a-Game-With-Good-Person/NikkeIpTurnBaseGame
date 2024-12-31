using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCheckBattleState : BattleState
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
    private Unit SelectPlayableUnit(bool EnemyTurn)
    {
        Unit ret = null;
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
            Debug.Log(unit.gameObject.name);
            if (unit.attackable || unit.movable)
            {
                ret = unit;
                break;
            }
        }
        return ret;
    }

    private void ResetTurn(bool EnemyTurn)
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
            unit.ResetAble();
        }
    }
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
        yield return null;

        //승리조건 체크함수

        if (owner.curControlUnit == null || !(owner.curControlUnit.attackable || owner.curControlUnit.movable))
        {
            owner.curControlUnit = SelectPlayableUnit(owner.enemyTurn);
            if (owner.curControlUnit == null)
            {
                owner.enemyTurn = !owner.enemyTurn;
                ResetTurn(owner.enemyTurn);
            }
        }
        
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
