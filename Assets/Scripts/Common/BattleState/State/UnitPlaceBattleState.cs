using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 유닛 배치 하는 상태
/// Place Unit on the map
/// <para>요구사항 1. 플레이어가 보유한 유닛들을 보여주는 UI 활성화</para>
/// <para>요구사항 2. 유닛을 드래그 해서 맵에 배치</para>
/// <para>요구사항 3 . 배치된 플레이어블 캐릭터가 없으면 게임을 시작할 수 없도록 제한</para>
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

        //그 타일에 유닛을 놓을수 있는지 검사
        if ((tile.tileState | TileState.Placeable) > 0){
            unit.transform.position = tile.center;

            //여기에 owner에 유닛을 등록하는 코드 넣어야함
        }
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}
