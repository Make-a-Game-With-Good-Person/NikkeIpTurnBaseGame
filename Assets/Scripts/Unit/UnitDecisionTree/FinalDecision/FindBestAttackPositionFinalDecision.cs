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
        Debug.Log("FindBestAttack");

        UnitMovement movement = owner.curControlUnit.GetComponent<UnitMovement>();

        List<Vector2Int> movables = movement.GetTilesInRange(owner.tileManager).ToList();

        Vector2Int pos = owner.curControlUnit.tile.coordinate;
        float max = ScorePosition(owner.curControlUnit.tile.coordinate);
        //max�� ã��
        foreach (Vector2Int temp in movables)
        {
            float cal = ScorePosition(temp);

            Debug.Log($"FindBestAttack, coordi : {temp}, score : {cal}");

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
                Debug.Log("FindBestAttack, halfcover");
                score *= 1.2f;
                break;
            }
            if (owner.tileManager.map[tilePos].fullCovers[i])
            {
                Debug.Log("FindBestAttack, fullcover");
                score *= 1.5f;
                break;
            }
        }


        /*
        //ù��° ��ų
        UnitSkill skill = owner.curControlUnit.unitSkills[0];
        //��ų ������
        HashSet<Vector2Int> skillrange = skill.GetSkillRange();


        foreach (Unit unit in owner.Units)
        {
            if (skillrange.Contains(unit.tile.coordinate))
            {
                //�ϴ� ���� �����Ҽ� �ִ� ��ġ�̱� ������ ���� ����
                score *= 1.5f;

                float distance = owner.tileManager.GetDistanceBetweenTiles(tilePos, unit.tile.coordinate);

                //�Ÿ��� �ݺ��
                score *= 10.0f / (distance + 10.0f);

                //Ÿ���� ������ �ִ����� ���� �ݺ��
                EDirection direction = owner.tileManager.GetDirection_B_to_A(tilePos, unit.tile.coordinate);

                if (owner.tileManager.map[unit.tile.coordinate].halfCovers[(int)direction])
                {
                    score *= 0.5f;
                    break;
                }
                if (owner.tileManager.map[tilePos].fullCovers[(int)direction])
                {
                    score *= 0.1f;
                    break;
                }
            }
        }
        */
        return score;
    }
}
