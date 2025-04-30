using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 필요한 사항들을 로딩하는 상태, 초기화도 여기서 하면 순서대로 할 수 있을듯?
/// </summary>
public class InitBattleState : BattleState
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
        StartCoroutine(Initializing());
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
    private IEnumerator Initializing() 
    {
        owner.tileIndicator.gameObject.SetActive(false);
        yield return null;
        //맵로딩
        //유닛 로딩
        //Units.Add(적유닛);
        foreach (Unit unit in owner.EnemyUnits)
        {
            if(unit != null)
            {
                Tile tile = owner.tileManager.GetTile(unit.transform.position);
                tile.Place(unit);
            }
        }
        //연출
        //카메라 무브 등등
        foreach (Unit unit in owner.Units)
        {
            if (unit != null)
            {
                Tile tile = owner.tileManager.GetTile(unit.transform.position);
                tile.Place(unit);
            }
        }


        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
