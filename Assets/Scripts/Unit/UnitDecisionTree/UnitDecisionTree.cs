using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DecisionTree;

public class UnitDecisionTree : MonoBehaviour
{
    private DecisionTreeNode decisionTree;

    #region FinalDecision
    TurnPassFinalDecision turnPassFinalDecision;
    SelectAbilityTargetFinalDecision selectAbilityTargetFinalDecision;
    FindBestAttackPositionFinalDecision findBestAttackPositionFinalDecision;
    FindBestCoverPositionFinalDecision findBestCoverPositionFinalDecision;
    #endregion

    #region Decision
    CheckTurnDecision checkTurnDecision;
    CheckAttackTurnDecision checkAttackTurnDecision;
    CheckMoveTurnDecision checkMoveTurnDecision;
    CheckReachableTargetDecision checkReachableTargetDecision;
    CheckChasingStateDecision checkChasingStateDecision;
    #endregion

    #region BattleManager, ReturnDecision, TargetFinder
    BattleManager owner;
    TargetFinder targetFinder;
    public ReturnDecision returnDecision;
    #endregion

    public DecisionTreeNode defaultDecisionTree;

    private void Awake()
    {
        owner = FindObjectOfType<BattleManager>();
        targetFinder = new TargetFinder(owner);
        returnDecision = new ReturnDecision();
    }

    private void Start()
    {
        turnPassFinalDecision = new TurnPassFinalDecision(returnDecision, owner);
        selectAbilityTargetFinalDecision = new SelectAbilityTargetFinalDecision(returnDecision, owner, targetFinder);
        findBestAttackPositionFinalDecision = new FindBestAttackPositionFinalDecision(returnDecision, owner);
        findBestCoverPositionFinalDecision = new FindBestCoverPositionFinalDecision(returnDecision, owner);

        checkChasingStateDecision = new CheckChasingStateDecision(owner, findBestAttackPositionFinalDecision, findBestCoverPositionFinalDecision);
        checkMoveTurnDecision = new CheckMoveTurnDecision(owner, checkChasingStateDecision, turnPassFinalDecision);
        checkReachableTargetDecision = new CheckReachableTargetDecision(owner, targetFinder,selectAbilityTargetFinalDecision, checkChasingStateDecision);
        checkAttackTurnDecision = new CheckAttackTurnDecision(owner, checkReachableTargetDecision, checkMoveTurnDecision);
        checkTurnDecision = new CheckTurnDecision(owner, checkAttackTurnDecision, turnPassFinalDecision);


        defaultDecisionTree = checkTurnDecision;
        SetDecisionTree(defaultDecisionTree);
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
