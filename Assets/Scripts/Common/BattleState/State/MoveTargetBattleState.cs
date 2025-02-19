using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̵������� �ݰ��� �����ִ� ����
/// <para>�䱸���� 1. ������ �̵��Ÿ�, ���� �ɷ�ġ�� ���� �� �� �ִ� Ÿ���� ������ ��</para>
/// <para>�䱸���� 2. �ڷΰ��� ��ư���� UnitSelectBattleState�� ����</para>
/// <para>�䱸���� 3. �̵� Ȯ�� ��ư���� MoveSequenceState�� ����</para>
/// </summary>
public class MoveTargetBattleState : BattleState
{
    #region Properties
    #region Private
    private HashSet<Vector2Int> range;
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
        if (owner.enemyTurn) return;
        owner.inputController.touchEvent.AddListener(OnTileSelect);
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        if (owner.enemyTurn) return;
        owner.inputController.touchEvent.RemoveListener(OnTileSelect);
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        if (!owner.enemyTurn)
        {
            owner.tileIndicator.gameObject.SetActive(true);
        }
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        owner.tileIndicator.gameObject.SetActive(false);
        owner.tileManager.TurnOffShowTiles();
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    protected void OnTileSelect(Vector3 pos)
    {
        if (pos.x < 0)
        {
            owner.tileIndicator.gameObject.SetActive(false);
            return;
        }
        owner.tileIndicator.gameObject.SetActive(true);

        if (range.Contains(owner.tileManager.GetTile(pos).coordinate))
        {
            //����Ŭ���ϸ�
            if (owner.tile == owner.tileManager.GetTile(pos))
            {
                owner.stateMachine.ChangeState<MoveSequenceState>();
                return;
            }
            owner.SelectTile(pos);
        }
    }
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;
        UnitMovement movement = owner.curControlUnit.GetComponent<UnitMovement>();
        range = movement.GetTilesInRange(owner.tileManager);

        owner.tileManager.ShowTiles(range);
        if (owner.enemyTurn)
        {
            owner.tile = owner.tileManager.GetTile(owner.curReturnDecision.pos);
            owner.stateMachine.ChangeState<MoveSequenceState>();
        }
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
