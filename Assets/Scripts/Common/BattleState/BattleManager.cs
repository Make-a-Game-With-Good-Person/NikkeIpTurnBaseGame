using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    [HideInInspector] public StateMachine stateMachine;
    public TileManager tileManager;

    public UnitPlacementUIController unitPlacementUIController;
    public AbilityMenuUIController abilityMenuUIController;
    public UnitDetailUIController unitdetailUIController;
    public InputController inputController;
    public CameraStateController cameraStateController; // 임시로 public으로 함

    public BATTLESTATE curState = BATTLESTATE.NONE; // 각 battleState를 알기 위해 만든 enum타입 변수

    public Unit selectedTarget; // 선택한 대상(적 유닛)
    public Unit curControlUnit; // 현재 플레이어가 선택한 아군 유닛
    public List<Unit> Units = new List<Unit>();

    public Transform tileIndicator; // 선택한 타일 표시하기 위한 아이콘같은것, 임시로 하나 만들어둠
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
    #endregion
    #region Public
    public void SelectTile(Vector3 pos)
    {
        if(tileIndicator == null)
        {
            Debug.LogError("BattleManager.SelectTile(), need tileIndicator");
            return;
        }

        Tile tile = this.tileManager.GetTile(pos);
        if (tile != null) 
        {
            tileIndicator.position = tile.center;
        }
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        stateMachine = gameObject.AddComponent<StateMachine>();
    }
    protected virtual void Start()
    {
        stateMachine.ChangeState<InitBattleState>();
    }
    #endregion
}
