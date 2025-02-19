using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCheckBattleState : BattleState
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
    private int CheckWin()
    {
        //��� ���� 0, �̰����� 1, ������ 2 return
        //������
        if (owner.Units.Count == 0)
        {
            return 2;
        }
        //�̰�����
        if(owner.EnemyUnits.Count == 0)
        {
            return 1;
        }
        //���� �¸��� �й赵 ����
        return 0;
    }

    private Unit SelectPlayableUnit(bool EnemyTurn)
    {
        Unit ret = null;
        List<Unit> unitList = null;
        if (EnemyTurn)
        {
            unitList = owner.EnemyUnits;
        }
        else
        {
            unitList = owner.Units;
        }

        foreach (Unit unit in unitList)
        {
            if (unit.attackable || unit.movable)
            {
                Debug.Log(unit.gameObject.name + "�� ���� ��" + unit.attackable +", " +unit.movable);
                ret = unit;
                break;
            }
        }
        return ret;
    }
    /// <summary>
    /// ���� ���� ���� ���ֵ��� ���� ��� �Ҹ��� ��뿡�� ���� �Ѱ��ִ� �Լ�
    /// ���࿡ �� �Լ����� ����/����� ���� ���� �پ��� �Ѵٸ�? 
    /// ���� ���� ���ƿ��� ������ ����ƴ� ����/������� �Ҹ��ϰ� �ϰ�
    /// �Ʊ��� ���� ���ƿ��� �Ʊ����� ����ƴ� ����/������� �Ҹ��ϰ� �Ѵ�?
    /// </summary>
    /// <param name="EnemyTurn"></param>
    private void ResetTurn(bool EnemyTurn)
    {
        List<Unit> unitList = null;
        if (EnemyTurn)
        {
            unitList = owner.EnemyUnits;
            owner.PlayerRoundEndEvent?.Invoke();
        }
        else
        {
            unitList = owner.Units;
            owner.touchedUnit = null;
            owner.EnemyRoundEndEvent?.Invoke();
        }

        foreach (Unit unit in unitList)
        {
            unit.ResetAble();
        }
    }
    #endregion
    #region Protected
    protected override void AddListeners()
    {
        base.AddListeners();
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
    }
    #endregion
    #region Public
    public override void Enter()
    {
        base.Enter();
        owner.tileManager.TurnOffShowTiles();
        StartCoroutine(ProcessingState());
    }
    public override void Exit()
    {
        base.Exit();
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    private IEnumerator ProcessingState()
    {
        yield return null;

        switch (CheckWin())
        {
            //��� ���� 0, �̰����� 1, ������ 2 return
            case 0:
                break;
            case 1:
                Debug.Log("�¸�");
                yield break;
            case 2:
                Debug.Log("�й�");
                yield break;
            default:
                break;
        }

        //�¸����� üũ�Լ�
        if(owner.curControlUnit != null && owner.curControlUnit.unitType == UnitType.Boss)
        {
            if (owner.curControlUnit.GetComponent<Boss>().turnTwice) // �� �������Ҷ�
            {
                owner.stateMachine.ChangeState<UnitSelectBattleState>(); // ���� ��Ʈ���ϰ� �ִ� ������ null�� �ȹٲٰ� �ٽ� ���ּ���Ʈ�� ������
                yield break;
            }
            else
            {
                Debug.Log("�������� ������ �� ������");
                owner.curControlUnit = SelectPlayableUnit(owner.enemyTurn);
                if (owner.curControlUnit == null)
                {
                    owner.enemyTurn = !owner.enemyTurn;
                    ResetTurn(owner.enemyTurn);
                }
            }
        }

        if (owner.curControlUnit == null || !(owner.curControlUnit.attackable || owner.curControlUnit.movable))
        {
            owner.curControlUnit = SelectPlayableUnit(owner.enemyTurn);
            if (owner.curControlUnit == null)
            {
                owner.enemyTurn = !owner.enemyTurn;
                ResetTurn(owner.enemyTurn);
            }
        }
        
        owner.stateMachine.ChangeState<UnitSelectBattleState>();
    }
    #endregion

    #region MonoBehaviour
    #endregion
}
