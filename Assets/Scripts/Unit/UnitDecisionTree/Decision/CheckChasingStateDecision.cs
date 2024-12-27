using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���� ������ �� ������ ���°� ���������� �ƴ��� Ȯ�� ��
/// ���� ���̶�� Ÿ���� ������ �� �ִ� ��ġ�� �̵��ϴ� ��������
/// �ƴ϶�� ������ �� �ִ� ������ ��ġ�� �̵��ϴ� ���� ���� ������ ���� ���
/// </summary>
public class CheckChasingStateDecision : Decision
{
    BattleManager owner;

    public CheckChasingStateDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) :
        base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if(owner.curControlUnit.unitState == EUnitState.CHASING)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
