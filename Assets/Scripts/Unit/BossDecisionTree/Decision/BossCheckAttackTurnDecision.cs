using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ ���� �Ͽ��� '����'�� �������� üũ�ϴ� �������
/// </summary>
public class BossCheckAttackTurnDecision : Decision
{
    Boss boss;

    public BossCheckAttackTurnDecision(Boss boss, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.boss = boss;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (boss.attackable)
        {
            Debug.Log($"������ ������ ������ {_trueNode}�� ����");
            return _trueNode;
        }
        else
        {
            Debug.Log($"������ ������ �Ұ����� {_falseNode}�� ����");
            return _falseNode;
        }
    }
}
