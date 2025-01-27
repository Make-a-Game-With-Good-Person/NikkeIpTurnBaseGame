using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    #region Controllers
    public UnitPlacementUIController unitPlacementUIController;
    public AbilityMenuUIController abilityMenuUIController;
    public UnitDetailUIController unitdetailUIController;
    public AbilityTargetUIController abilityTargetUIController;
    public InputController inputController;
    public CameraStateController cameraStateController; // �ӽ÷� public���� ��
    public SelectSkillTargetUIController selectSkillTargetUIController;
    public ConfirmAbilityTargetUIController confirmAbilityTargetUIController;
    public DecisionTreeManager decisionTreeManager;
    #endregion


    public BATTLESTATE curState = BATTLESTATE.NONE; // �� battleState�� �˱� ���� ���� enumŸ�� ����

    public GameObject selectedTarget; // ������ ���(�� ����)
    public Unit curControlUnit; // ���� �÷��̾ ������ �Ʊ� ����
    public List<Unit> Units = new List<Unit>(); //�Ʊ��� ���� ���� ���θ� ���⿡ ������ �뵵
    public List<Unit> EnemyUnits = new List<Unit>();
    public Tile tile;   //���� ������ Ÿ���� ������ �뵵, SelectTile�Լ����� �����Ұ�
    public UnitSkill curSelectedSkill;
    public ReturnDecision curReturnDecision;
    public bool enemyTurn = false; //�������� �Ʊ������� Ȯ���� �뵵

    public HashSet<Vector2Int> selectedSkillRangeTile;

    public Transform tileIndicator; // ������ Ÿ�� ǥ���ϱ� ���� �����ܰ�����, �ӽ÷� �ϳ� ������
    public LayerMask abilityTargetMask; // ���� ����� Ư���ϴ� ���̾� ����ũ

    public int skillIdx;
    #endregion
    #region Events
    public UnityEvent RoundEndEvent = new UnityEvent();
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

    public void CheckTurn()
    {

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
