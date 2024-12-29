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
        //max°ª Ã£±â
        Vector2Int pos = movables.Aggregate((best, current) => ScorePosition(current) > ScorePosition(best) ? current : best);

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
