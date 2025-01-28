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
    private int CheckWin()
    {
        //계속 진행 0, 이겼을때 1, 졌을때 2 return
        //졌을때
        if (owner.Units.Count == 0)
        {
            return 2;
        }
        //이겼을때
        if(owner.EnemyUnits.Count == 0)
        {
            return 1;
        }
        //아직 승리도 패배도 안함
        return 0;
    }

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

        switch (CheckWin())
        {
            //계속 진행 0, 이겼을때 1, 졌을때 2 return
            case 0:
                break;
            case 1:
                Debug.Log("승리");
                yield break;
                break;
            case 2:
                Debug.Log("패배");
                yield break;
                break;
        }

        //승리조건 체크함수
        if(owner.curControlUnit != null && owner.curControlUnit.unitType == UnitType.Boss)
        {
            if (owner.curControlUnit.GetComponent<Boss>().turnTwice) // 턴 재진입할때
            {
                owner.stateMachine.ChangeState<UnitSelectBattleState>(); // 현재 컨트롤하고 있는 보스를 null로 안바꾸고 다시 유닛셀렉트로 재진입
                yield break;
            }
            else
            {
                owner.curControlUnit = SelectPlayableUnit(owner.enemyTurn);
                if (owner.curControlUnit == null)
                {
                    owner.enemyTurn = !owner.enemyTurn;
                    ResetTurn(owner.enemyTurn);
                }
            }
        }

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
