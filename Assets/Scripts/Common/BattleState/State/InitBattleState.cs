using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ʿ��� ���׵��� �ε��ϴ� ����, �ʱ�ȭ�� ���⼭ �ϸ� ������� �� �� ������?
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
        //�ʷε�
        //���� �ε�
        //Units.Add(������);
        foreach (Unit unit in owner.EnemyUnits)
        {
            if(unit != null)
            {
                Tile tile = owner.tileManager.GetTile(unit.transform.position);
                tile.Place(unit);
            }
        }
        //����
        //ī�޶� ���� ���
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
