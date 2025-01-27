using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReturnDecision;

public class DecisionTreeManager : MonoBehaviour
{
    [SerializeField] UnitDecisionTree enemyDecisionTree;
    [SerializeField] BossDecisionTree bossDecisionTree;

    ReturnDecision returnDecision;

    public ReturnDecision RunAI(UnitType unitType)
    {
        if (unitType == UnitType.Enemy)
        {
            returnDecision = enemyDecisionTree.Run();
        }
        else if(unitType == UnitType.Boss)
        {
            returnDecision = bossDecisionTree.Run();
        }

        return returnDecision;
    }
}
