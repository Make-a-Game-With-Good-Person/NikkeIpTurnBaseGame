using DecisionTree;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CheckContactDecision : Decision
{
    BattleManager owner;
    ConeTileCalculator conTileCalculator;

    public CheckContactDecision(BattleManager owner, DecisionTreeNode trueNode, DecisionTreeNode falseNode) : base(trueNode, falseNode)
    {
        this.owner = owner;
        conTileCalculator = new ConeTileCalculator();
    }
    public override DecisionTreeNode GetBranch()
    {
        if (CheckAlreadyContacted() || CheckContactable())
        {
            owner.EndBuffEvent?.Invoke();
            return _trueNode;
        }
        else
        {
            return _falseNode;
        }
    }

    bool CheckAlreadyContacted()
    {
        foreach (Unit enemy in owner.EnemyUnits)
        {
            if (enemy.isContacted) return true;
        }

        return false;
    }

    bool CheckContactable()
    {
        List<Tile> tiles = conTileCalculator.GetTilesInRange(owner);

        foreach (Tile item in tiles)
        {
            if (owner.tileManager.tilesForShow.ContainsKey(item.coordinate))
                owner.tileManager.tilesForShow[item.coordinate].SetActive(true);

        }

        foreach (Unit player in owner.Units)
        {
            if (tiles.Contains(player.tile))
            {
                foreach (Unit enemy in owner.EnemyUnits)
                {
                    enemy.isContacted = true;
                }

                return true;
            }
        }

        return false;
    }

}
