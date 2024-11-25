using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    protected Unit unit;
    protected HashSet<Vector2Int> range = new HashSet<Vector2Int>();
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
        range = map.SearchTile(unit.tile.coordinate, ExpandSearch);
        Filter(range, map);
        return range;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    public virtual IEnumerator Traverse(Vector2Int end ,TileManager map)
    {
        if(range.Count < 0)
        {
            GetTilesInRange(map);
        }

        unit.tile.UnPlace(unit);

        //일단 무조건 대각선이 아니라 직각으로 이동
        if(!range.Contains(end))
        {
            yield break;
        }
         
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int tile = end;
        while (tile != map.map[tile].prev){
            path.Insert(0, tile);
            tile = map.map[tile].prev;
        }
        //start 지점 추가
        path.Insert(0, tile);

        for(int i = 1; i < path.Count; i++)
        {
            int height = Math.Clamp(map.map[path[i - 1]].height - map.map[path[i]].height, -1, 1);

            yield return StartCoroutine(Turning(map.map[path[i]].center));

            switch (height) 
            {
                case -1:
                    yield return StartCoroutine(Climbing(map.map[path[i]].center));
                    break;
                case 0:
                    yield return StartCoroutine(Running(map.map[path[i]].center));
                    break;
                case 1:
                    yield return StartCoroutine(Jumping(map.map[path[i]].center));
                    break;
            }
        }

        map.map[path[path.Count - 1]].Place(unit);
    }

    protected IEnumerator Running(Vector3 to)
    {
        Vector3 dir = to - unit.gameObject.transform.position;
        float dist = dir.magnitude;
        float delta = 0;
        dir.Normalize();
        //여기에 애니메이션 파라미터 수정

        while (dist > 0)
        {
            delta = 1.0f * Time.deltaTime;

            if(delta > dist)
            {
                delta = dist;
            }

            unit.gameObject.transform.Translate(delta * dir);
            dist -= delta;
            yield return null;
        }
    }

    //아래로 점프
    protected IEnumerator Jumping(Vector3 to)
    {
        yield return null;
    }

    //위로 기어 올라감
    protected IEnumerator Climbing(Vector3 to)
    {
        yield return null;
    }

    protected IEnumerator Turning(Vector3 to)
    {
        Vector3 dir = to - unit.transform.position;
        

        float angle = Vector3.Angle(unit.transform.forward, dir);
        float right = Vector3.Dot(unit.transform.right, dir) < 0 ? -1f : 1f;
        float delta = 0;

        while(angle > 0)
        {
            delta = 60.0f * Time.deltaTime;

            if(delta > angle)
            {
                delta = angle;
            }

            unit.transform.Rotate(delta * right * Vector3.up);
            angle -= delta;
            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        unit = GetComponent<Unit>();
    }
    #endregion
}

