using UnityEngine;
using System.Collections;

namespace DecisionTree
{
    public abstract class DecisionTreeNode
    {
        public abstract DecisionTreeNode MakeDecision(); 
    }

    public abstract class FinalDecision : DecisionTreeNode
    {
        public override DecisionTreeNode MakeDecision()
        {
            return this;
        }

        public abstract void Execute();
    }

    public abstract class Decision : DecisionTreeNode
    {
        protected DecisionTreeNode _trueNode;
        protected DecisionTreeNode _falseNode;

        public Decision(DecisionTreeNode trueNode, DecisionTreeNode falseNode)
        {
            _trueNode = trueNode;
            _falseNode = falseNode;
        }

        abstract public DecisionTreeNode GetBranch();

        public override DecisionTreeNode MakeDecision()
        {
            DecisionTreeNode branch = GetBranch();
            return branch.MakeDecision();
        }
    }
}
