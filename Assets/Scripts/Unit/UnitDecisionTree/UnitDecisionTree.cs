using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DecisionTree;

public class UnitDecisionTree : MonoBehaviour
{
    private DecisionTreeNode decisionTree;

    #region FinalDecision
    AlertFinalDecision alertFinalDecision;
    TurnPassFinalDecision turnPassFinalDecision;
    SelectAbilityTargetFinalDecision selectAbilityTargetFinalDecision;
    FindBestAttackPositionFinalDecision findBestAttackPositionFinalDecision;
    FindBestCoverPositionFinalDecision findBestCoverPositionFinalDecision;
    #endregion

    #region Decision
    CheckContactDecision checkContactDecision;
    CheckTurnDecision checkTurnDecision;
    CheckAttackTurnDecision checkAttackTurnDecision;
    CheckMoveTurnDecision checkMoveTurnDecision;
    CheckReachableTargetDecision checkReachableTargetDecision;
    CheckChasingStateDecision checkChasingStateDecision;
    #endregion

    #region BattleManager, ReturnDecision
    BattleManager owner;
    public ReturnDecision returnDecision;
    #endregion

    public DecisionTreeNode defaultDecisionTree;

    private void Awake()
    {
        owner = FindObjectOfType<BattleManager>();
        //returnDecision = new ReturnDecision();

    }

    private void Start()
    {
        alertFinalDecision = new AlertFinalDecision(returnDecision,owner);
        turnPassFinalDecision = new TurnPassFinalDecision(returnDecision, owner);
        selectAbilityTargetFinalDecision = new SelectAbilityTargetFinalDecision(returnDecision, owner);
        findBestAttackPositionFinalDecision = new FindBestAttackPositionFinalDecision(returnDecision, owner);
        findBestCoverPositionFinalDecision = new FindBestCoverPositionFinalDecision(returnDecision, owner);

        checkChasingStateDecision = new CheckChasingStateDecision(owner, findBestAttackPositionFinalDecision, findBestCoverPositionFinalDecision);
        checkMoveTurnDecision = new CheckMoveTurnDecision(owner, checkChasingStateDecision, turnPassFinalDecision);
        checkReachableTargetDecision = new CheckReachableTargetDecision(owner, selectAbilityTargetFinalDecision, checkMoveTurnDecision);
        checkAttackTurnDecision = new CheckAttackTurnDecision(owner, checkReachableTargetDecision, checkMoveTurnDecision);
        checkTurnDecision = new CheckTurnDecision(owner, checkAttackTurnDecision, turnPassFinalDecision);
        checkContactDecision = new CheckContactDecision(owner, checkTurnDecision, alertFinalDecision);

        defaultDecisionTree = checkContactDecision;
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
