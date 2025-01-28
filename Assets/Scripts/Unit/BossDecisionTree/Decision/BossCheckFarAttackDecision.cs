using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ ������ ������ ��, �հŸ� Ÿ�� ��ų�� ��� �������� üũ�ϴ� �������
/// ��ų�� ��� ���������� ��ų�� ��Ÿ���� ���� �Ǵ��Ѵ�.
/// </summary>
public class BossCheckFarAttackDecision : Decision
{
    Boss boss;

    public BossCheckFarAttackDecision(Boss boss, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.boss = boss;
    }

    public override DecisionTreeNode GetBranch()
    {
        if (!boss.unitSkills[0].GetComponent<BossSkill>().coolCheck)
        {
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }
}
