using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected Unit unit;
    #endregion
    #region public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    /// <summary>
    /// 타일 매니저에서 쓸 코스트 계산용 함수
    /// </summary>
    /// <param name="from">current tile</param>
    /// <param name="to">tile which is try to be add</param>
    /// <returns></returns>
    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + to.cost <= unit[EStatType.Move]) && (Math.Abs(from.height - to.height) <= unit[EStatType.Jump]);
    }

    protected virtual void Filter(HashSet<Vector2Int> range, TileManager map)
    {
        HashSet<Vector2Int> temp = new HashSet<Vector2Int>();
        foreach(Vector2Int node in range)
        {
            if (map.map[node].content != null)
                temp.Add(node);
        }
        foreach(Vector2Int node in temp)
        {
            range.Remove(node);
        }
    }
    #endregion
    #region Public
    public virtual HashSet<Vector2Int> GetTilesInRange(TileManager map)
    {
        HashSet<Vector2Int> range = map.SearchTile(unit.tile.coordinate, ExpandSearch);
        Filter(range, map);
        return range;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        unit = GetComponent<Unit>();
    }
    #endregion
}

