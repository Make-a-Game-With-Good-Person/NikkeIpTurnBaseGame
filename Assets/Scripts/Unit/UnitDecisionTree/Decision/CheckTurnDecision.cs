using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �� ������ ����/�̵� �� �� �ϳ��� ������ �ִ��� Ȯ���ϴ� ���� ���
/// �ϳ��� �����Ѵٸ� ���� ���� �����ϰ� �ִ��� Ȯ���ϴ� ���� ����
/// �Ѵ� ���ٸ� ���� �����ϴ� ���� ���� �̵�
/// </summary>
public class CheckTurnDecision : Decision
{
    BattleManager owner;

    public CheckTurnDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if(owner.curControlUnit.attackable || owner.curControlUnit.movable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
