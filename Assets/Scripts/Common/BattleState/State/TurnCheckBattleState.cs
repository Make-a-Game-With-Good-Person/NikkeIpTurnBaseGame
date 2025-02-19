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
            if (unit.attackable || unit.movable)
            {
                Debug.Log(unit.gameObject.name + "가 선택 됨" + unit.attackable +", " +unit.movable);
                ret = unit;
                break;
            }
        }
        return ret;
    }
    /// <summary>
    /// 현재 본인 진영 유닛들의 턴을 모두 소모해 상대에게 턴을 넘겨주는 함수
    /// 만약에 이 함수에서 버프/디버프 지속 턴을 줄어들게 한다면? 
    /// 적의 턴이 돌아오면 적에게 적용됐던 버프/디버프를 소모하게 하고
    /// 아군의 턴이 돌아오면 아군에게 적용됐던 버프/디버프를 소모하게 한다?
    /// </summary>
    /// <param name="EnemyTurn"></param>
    private void ResetTurn(bool EnemyTurn)
    {
        List<Unit> unitList = null;
        if (EnemyTurn)
        {
            unitList = owner.EnemyUnits;
            owner.PlayerRoundEndEvent?.Invoke();
        }
        else
        {
            unitList = owner.Units;
            owner.touchedUnit = null;
            owner.EnemyRoundEndEvent?.Invoke();
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
        owner.tileManager.TurnOffShowTiles();
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
            case 2:
                Debug.Log("패배");
                yield break;
            default:
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
                Debug.Log("보스턴이 재진입 후 끝났음");
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
