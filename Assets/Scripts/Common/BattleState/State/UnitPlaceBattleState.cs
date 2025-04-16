using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���� ��ġ �ϴ� ����
/// Place Unit on the map
/// <para>�䱸���� 1. �÷��̾ ������ ���ֵ��� �����ִ� UI Ȱ��ȭ</para>
/// <para>�䱸���� 2. ������ �巡�� �ؼ� �ʿ� ��ġ</para>
/// <para>�䱸���� 3. ��ġ�� �÷��̾�� ĳ���Ͱ� ������ ������ ������ �� ������ ����</para>
/// <para>�䱸���� 4. �̹� ��ġ�� ������ �巡�� �ؼ� �ٸ� ��ġ�� �̵���Ű�� </para>
/// </summary>
public class UnitPlaceBattleState : BattleState
{
    /*  12-03
     *  1. ���� ������ ���� �ٸ����� ���� ������ ������ ù Ÿ�Ͽ��� unplace()�� ������ ���ؼ� ������ �ű⿡ �ִٰ� �����ϴ� ���� ����
     *      - 12-04 �ذ�
     *  2. ������ ���� Ÿ�Ͽ��� �������� ���� ����
     *      - 12-03 �ذ�
     */
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region public
    #endregion
    #region Events
    private UnityEvent<Unit> failToPlaceEvent = new UnityEvent<Unit>();
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

        failToPlaceEvent.AddListener(owner.unitPlacementUIController.OnFailToPlace);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        owner.unitPlacementUIController.confirmButton.onClick.RemoveListener(OnGameStartButton);
        owner.unitPlacementUIController.setUnitEvent.RemoveListener(OnUnitPlace);

        failToPlaceEvent.RemoveListener(owner.unitPlacementUIController.OnFailToPlace);
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
    
    public void OnUnitPlace(Unit unit, Vector3 worldPos)
    {
        Debug.Log("UnitPlaceBattleState.OnUnitPlace");

        Tile tile = owner.tileManager.GetTile(worldPos);

        //�� Ÿ�Ͽ� ������ ������ �ִ��� �˻�
        if ((tile.tileState & TileState.Placeable) > 0){
            unit.transform.position = tile.center;
            //���⿡ owner�� ������ ����ϴ� �ڵ� �־����
            tile.Place(unit);
            if(!owner.Units.Contains(unit))
                owner.Units.Add(unit);
        }
        else
        {
            failToPlaceEvent?.Invoke(unit);
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
