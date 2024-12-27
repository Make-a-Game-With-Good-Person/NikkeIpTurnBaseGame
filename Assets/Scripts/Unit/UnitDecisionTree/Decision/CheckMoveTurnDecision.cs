using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �� ������ �̵����� �����ϰ� �ִ��� Ȯ���ϴ� ���� ���
/// �̵� ���� �����ϰ� �ִٸ� ���� ���� ���� ���¸� Ȯ���ϴ� ���� ����
/// �̵� ���� ���ٸ� ���� �����ϴ� ���� ���� �̵�
/// </summary>
public class CheckMoveTurnDecision : Decision
{
    BattleManager owner;

    public CheckMoveTurnDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode,falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (owner.curControlUnit.movable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
