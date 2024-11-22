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
    public CameraStateController cameraStateController; // 임시로 public으로 함
    public BATTLESTATE curState = BATTLESTATE.NONE; // 각 battleState를 알기 위해 만든 enum타입 변수
    public Unit selectedTarget; // 선택한 대상(적 유닛)
    public Unit curControlUnit; // 현재 플레이어가 선택한 아군 유닛
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
