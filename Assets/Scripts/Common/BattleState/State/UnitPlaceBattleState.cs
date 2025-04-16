using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 유닛 배치 하는 상태
/// Place Unit on the map
/// <para>요구사항 1. 플레이어가 보유한 유닛들을 보여주는 UI 활성화</para>
/// <para>요구사항 2. 유닛을 드래그 해서 맵에 배치</para>
/// <para>요구사항 3. 배치된 플레이어블 캐릭터가 없으면 게임을 시작할 수 없도록 제한</para>
/// <para>요구사항 4. 이미 배치된 유닛을 드래그 해서 다른 위치로 이동시키면 </para>
/// </summary>
public class UnitPlaceBattleState : BattleState
{
    /*  12-03
     *  1. 현재 유닛을 놓고 다른데에 같은 유닛을 놓으면 첫 타일에서 unplace()를 실행을 안해서 여전히 거기에 있다고 판정하는 버그 있음
     *      - 12-04 해결
     *  2. 놓을수 없는 타일에도 놓여지는 버그 있음
     *      - 12-03 해결
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

        //그 타일에 유닛을 놓을수 있는지 검사
        if ((tile.tileState & TileState.Placeable) > 0){
            unit.transform.position = tile.center;
            //여기에 owner에 유닛을 등록하는 코드 넣어야함
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
