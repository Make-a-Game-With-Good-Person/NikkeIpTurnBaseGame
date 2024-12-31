using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindBestCoverPositionFinalDecision : FinalDecision
{
    ReturnDecision returnDecision;
    BattleManager owner;

    public FindBestCoverPositionFinalDecision(ReturnDecision returnDecision, BattleManager owner)
    {
        this.returnDecision = returnDecision;
        this.owner = owner;
    }

    public override ReturnDecision Execute()
    {
        UnitMovement movement = owner.curControlUnit.GetComponent<UnitMovement>();

        HashSet<Vector2Int> movables = movement.GetTilesInRange(owner.tileManager);

        Vector2Int pos = owner.curControlUnit.tile.coordinate;
        float max = ScorePosition(owner.curControlUnit.tile.coordinate);
        //max°ª Ã£±â
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
        for(int i = 0; i < 4; i++)
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

        return score;
    }
}
