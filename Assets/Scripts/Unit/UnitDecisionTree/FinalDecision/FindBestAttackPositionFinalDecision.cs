using DecisionTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 현재 알고리즘에서 적에게 근접하는 쪽의 타일에 스코어를 더 주게 되어있음
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
        //max값 찾기
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


        
        //첫번째 스킬
        UnitSkill skill = owner.curControlUnit.unitSkills[0];
        //스킬 레인지
        HashSet<Vector2Int> skillrange = skill.GetSkillRange();


        foreach (Unit unit in owner.Units)
        {
            if (skillrange.Contains(unit.tile.coordinate))
            {
                //일단 적을 공격할수 있는 위치이기 때문에 높은 점수
                score *= 1.5f;

                float distance = owner.tileManager.GetDistanceBetweenTiles(tilePos, unit.tile.coordinate);

                //거리와 반비례
                score *= 10.0f / (distance + 10.0f);

                //타겟의 엄폐물이 있는지에 따라 반비례
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
        
        return score;
    }
}
