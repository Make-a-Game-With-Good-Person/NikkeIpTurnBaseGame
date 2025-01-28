using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReturnDecision;
using static UnityEngine.UI.GridLayoutGroup;

public class BossDecisionTree : MonoBehaviour
{
    #region FinalDecision
    BossAttackFartherTargetFinalDecision attackFartherTargetFinalDecision;
    BossAttackNearestTargetFinalDecision attackNearestTargetFinalDecision;
    BossPassAttackFinalDecision passAttackFinalDecision;
    BossSummonMonsterFinalDecision summonMonsterFinalDecision;
    BossPassTurnFinalDecision passTurnFinalDecision;
    #endregion

    #region Decision
    BossCheckAttackTurnDecision checkAttackTurnDecision;
    BossCheckFarAttackDecision checkFarAttackDecision;
    BossCheckNearAttackDecision checkNearAttackDecision;
    BossCheckSummonDecision checkSummonDecision;
    #endregion

    public ReturnDecision returnDecision;
    private DecisionTreeNode decisionTree;
    public DecisionTreeNode defaultDecisionTree;
    BattleManager owner;
    Boss boss;

    private void Awake()
    {
        owner = FindObjectOfType<BattleManager>();
        boss = FindObjectOfType<Boss>();
        //returnDecision = new ReturnDecision();
    }
    // Start is called before the first frame update
    void Start()
    {
        attackFartherTargetFinalDecision = new BossAttackFartherTargetFinalDecision(returnDecision, owner);
        attackNearestTargetFinalDecision = new BossAttackNearestTargetFinalDecision(returnDecision, owner);
        passAttackFinalDecision = new BossPassAttackFinalDecision(returnDecision);
        summonMonsterFinalDecision = new BossSummonMonsterFinalDecision(returnDecision, owner, boss);
        passTurnFinalDecision = new BossPassTurnFinalDecision(returnDecision, boss);

        checkSummonDecision = new BossCheckSummonDecision(boss, summonMonsterFinalDecision, passTurnFinalDecision);
        checkNearAttackDecision = new BossCheckNearAttackDecision(boss, attackNearestTargetFinalDecision, passAttackFinalDecision);
        checkFarAttackDecision = new BossCheckFarAttackDecision(boss, attackFartherTargetFinalDecision, checkNearAttackDecision);
        checkAttackTurnDecision = new BossCheckAttackTurnDecision(boss, checkFarAttackDecision, checkSummonDecision);



        defaultDecisionTree = checkAttackTurnDecision;
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
