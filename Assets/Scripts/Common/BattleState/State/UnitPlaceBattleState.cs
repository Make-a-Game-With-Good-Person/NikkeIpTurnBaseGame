using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ��ġ �ϴ� ����
/// Place Unit on the map
/// <para>�䱸���� 1. �÷��̾ ������ ���ֵ��� �����ִ� UI Ȱ��ȭ</para>
/// <para>�䱸���� 2. ������ �巡�� �ؼ� �ʿ� ��ġ</para>
/// <para>�䱸���� 3 . ��ġ�� �÷��̾�� ĳ���Ͱ� ������ ������ ������ �� ������ ����</para>
/// </summary>
public class UnitPlaceBattleState : BattleState
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
        owner.unitPlacementUIController.confirmButton.onClick.AddListener(OnGameStartButton);
        owner.unitPlacementUIController.setUnitEvent.AddListener(OnUnitPlace);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.unitPlacementUIController.confirmButton.onClick.RemoveListener(OnGameStartButton);
        owner.unitPlacementUIController.setUnitEvent.RemoveListener(OnUnitPlace);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.unitPlacementUIController.Display();
    }
    public override void Exit()
    {
        base.Exit();
        owner.unitPlacementUIController.Hide();
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnGameStartButton()
    {
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    
    public void OnUnitPlace(GameObject unit, Vector3 worldPos)
    {
        Tile tile = owner.tileManager.GetTile(worldPos);

        //�� Ÿ�Ͽ� ������ ������ �ִ��� �˻�
        if ((tile.tileState | TileState.Placeable) > 0){
            unit.transform.position = tile.center;

            //���⿡ owner�� ������ ����ϴ� �ڵ� �־����
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
