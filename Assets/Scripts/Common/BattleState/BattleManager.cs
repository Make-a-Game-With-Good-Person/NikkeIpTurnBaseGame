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
    public AbilityTargetUIController abilityTargetUIController;
    public InputController inputController;
    public CameraStateController cameraStateController; // 임시로 public으로 함
    public SelectSkillTargetUIController selectSkillTargetUIController;

    public BATTLESTATE curState = BATTLESTATE.NONE; // 각 battleState를 알기 위해 만든 enum타입 변수

    public Unit selectedTarget; // 선택한 대상(적 유닛)
    public Unit curControlUnit; // 현재 플레이어가 선택한 아군 유닛
    public List<Unit> Units = new List<Unit>(); //아군과 적군 유닛 전부를 여기에 저장할 용도
    public Tile tile;   //현재 선택한 타일을 저장할 용도, SelectTile함수에서 지정할것
    public UnitSkill curSelectedSkill;

    public HashSet<Vector2Int> selectedSkillRangeTile;

    public Transform tileIndicator; // 선택한 타일 표시하기 위한 아이콘같은것, 임시로 하나 만들어둠
    public LayerMask abilityTargetMask; // 공격 대상을 특정하는 레이어 마스크
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
            this.tile = tile;
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
