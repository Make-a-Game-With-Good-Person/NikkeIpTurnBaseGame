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
    public CameraStateController cameraStateController; // �ӽ÷� public���� ��

    public BATTLESTATE curState = BATTLESTATE.NONE; // �� battleState�� �˱� ���� ���� enumŸ�� ����

    public Unit selectedTarget; // ������ ���(�� ����)
    public Unit curControlUnit; // ���� �÷��̾ ������ �Ʊ� ����
    public List<Unit> Units = new List<Unit>();

    public Transform tileIndicator; // ������ Ÿ�� ǥ���ϱ� ���� �����ܰ�����, �ӽ÷� �ϳ� ������
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
