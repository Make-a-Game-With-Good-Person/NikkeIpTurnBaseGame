using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeTileCalculator
{
    public bool directionOriented { get { return true; } }
    public List<Tile> GetTilesInRange(BattleManager owner)
    {
        Vector3 pos = owner.curControlUnit.tile.worldPos;
        List<Tile> retValue = new List<Tile>();
        int dir = (owner.curControlUnit.transform.forward == Vector3.forward || owner.curControlUnit.transform.forward == Vector3.right) ? 1 : -1;
        int lateral = 1;
        if (owner.curControlUnit.transform.forward == Vector3.forward || owner.curControlUnit.transform.forward == Vector3.back)
        {
            for (int y = 1; y <= owner.curControlUnit[EStatType.Visual]; y++)
            {
                int min = -(lateral / 2);
                int max = (lateral / 2);
                for (int x = min; x <= max+1; x++)
                {
                    Vector3 nxt = new Vector3(pos.x + x, owner.curControlUnit.transform.position.y, pos.z + (y * dir));
                    Tile tile = owner.tileManager.GetTile(nxt);
                    if (ValidTile(owner, tile) && !(retValue.Contains(tile)))
                    {
                        retValue.Add(tile);
                    }
                }
                lateral += 2;
            }
        }
        else
        {
            for (int x = 1; x <= owner.curControlUnit[EStatType.Visual]; x++)
            {
                int min = -(lateral / 2);
                int max = (lateral / 2);
                for (int y = min; y <= max+1; y++)
                {
                    Vector3 nxt = new Vector3(pos.x + (x*dir), owner.curControlUnit.transform.position.y,pos.z + y );
                    Tile tile = owner.tileManager.GetTile(nxt);
                    if (ValidTile(owner,tile) && !(retValue.Contains(tile)))
                    {
                        retValue.Add(tile);
                    }
                }
                lateral += 2;
            }
        }
        return retValue;
    }
    bool ValidTile(BattleManager owner, Tile t)
    {
        return t != null && t.height <= owner.curControlUnit[EStatType.Jump];
    }
}
