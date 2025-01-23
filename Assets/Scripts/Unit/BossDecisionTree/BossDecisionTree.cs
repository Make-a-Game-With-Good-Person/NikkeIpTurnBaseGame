using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ReturnDecision;

public class BossDecisionTree : MonoBehaviour
{
    #region FinalDecision
    #endregion

    #region Decision
    #endregion
    private DecisionTreeNode decisionTree;
    public DecisionTreeNode defaultDecisionTree;
    // Start is called before the first frame update
    void Start()
    {
        
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
