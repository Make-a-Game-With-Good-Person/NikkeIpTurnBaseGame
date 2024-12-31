using DecisionTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// ���� �˰��򿡼� ������ �����ϴ� ���� Ÿ�Ͽ� ���ھ �� �ְ� �Ǿ�����
/// </summary>
public class FindBestAttackPositionFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;

    public FindBestAttackPositionFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        UnitMovement movement = owner.curControlUnit.GetComponent<UnitMovement>();

        List<Vector2Int> movables = movement.GetTilesInRange(owner.tileManager).ToList();

        Vector2Int pos = owner.curControlUnit.tile.coordinate;
        float max = -100;
        //max�� ã��
        foreach (Vector2Int temp in movables)
        {
            float cal = ScorePosition(temp);
            if (max < cal)
            {
                max = cal;
                pos = temp;
            }
        }

        returnDecision.type = ReturnDecision.DecisionType.Move;
        returnDecision.pos = pos;

        return returnDecision;
    }

    private float ScorePosition(Vector2Int tilePos)
    {
        float score = 1;
        for (int i = 0; i < 4; i++)
        {
            if (owner.tileManager.map[tilePos].halfCovers[i])
            {
                score *= 1.2f;
                break;
            }
            if (owner.tileManager.map[tilePos].fullCovers[i])
            {
                score *= 1.5f;
                break;
            }
        }

        //ù��° ��ų
        UnitSkill skill = owner.curControlUnit.unitSkills[0];
        //��ų ������
        HashSet<Vector2Int> skillrange = owner.tileManager.SearchTile(tilePos, (from, to) =>
        { return from.distance + 1 <= owner.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= owner.curSelectedSkill.skillHeight; }
        );

        foreach (Unit unit in owner.EnemyUnits)
        {
            if (skillrange.Contains(unit.tile.coordinate))
            {
                //�ϴ� ���� �����Ҽ� �ִ� ��ġ�̱� ������ ���� ����
                score *= 2.0f;

                float distance = owner.tileManager.GetDistanceBetweenTiles(tilePos, unit.tile.coordinate);

                //�Ÿ��� �ݺ��
                score *= 10.0f / (distance + 10.0f);
            }
        }

        return score;
    }
}
