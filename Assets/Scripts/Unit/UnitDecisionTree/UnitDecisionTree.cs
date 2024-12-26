using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DecisionTree;

public class UnitDecisionTree : MonoBehaviour
{
    private DecisionTreeNode decisionTree;

    #region FinalDecision
    //TurnPassFinalDecision turnPassFinalDecision;
    //SelectAbilityTargetFinalDecision selectAbilityTargetFinalDecision;
    //FindBestAttackPositionFinalDecision findBestAttackPositionFinalDecision;
    //FindBestCoverPositionFinalDecision findBestCoverPositionFinalDecision;
    #endregion

    #region Decision
    //CheckTurnDecision checkTrunDecision;
    //CheckAttackTurnDecision checkAttackTurnDecision;
    //CheckMoveTurnDecision checkMoveTurnDecision;
    //CheckReachableTargetDecision checkReachableTargetDecision;
    //CheckChasingStateDecision checkChasingStateDecision;
    #endregion

    public DecisionTreeNode defaultDecisionTree;

    private void Awake()
    {
        
    }

    private void Start()
    {


        //defaultDecisionTree = CheckTurnDecision;
        //SetDecisionTree(defaultDecisionTree);
    }

    public ReturnDecision Run()
    {
        return ((FinalDecision)decisionTree.MakeDecision()).Execute();
    }

    public void SetDecisionTree(DecisionTreeNode decisionTree)
    {
        this.decisionTree = decisionTree;
    }
}
