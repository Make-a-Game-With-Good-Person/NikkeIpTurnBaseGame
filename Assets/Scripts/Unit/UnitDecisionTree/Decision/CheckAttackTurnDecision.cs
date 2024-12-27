using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ �� ������ ���� ���� �����ϰ� �ִ��� Ȯ���ϴ� ���� ���
/// ���� ���� �����ϰ� �ִٸ� �� ������ ��ų ���� �ȿ� Ÿ���� �����ϴ��� Ȯ���ϴ� ���� ����
/// �����ϰ� ���� �ʴٸ� �̵� �� ���� ���θ� Ȯ���ϴ� ���� ���� �̵�
/// </summary>
public class CheckAttackTurnDecision : Decision
{
    BattleManager owner;

    public CheckAttackTurnDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.owner = owner;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (owner.curControlUnit.attackable)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
