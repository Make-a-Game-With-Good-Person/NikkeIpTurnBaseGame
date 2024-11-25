using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

//메모
//1. 무조건 0, 0, 0이 기점
//2. 한칸 크기를 Vector3로 입력, y축은 높이로 사용
//3. map에서 저장방식을 Vector3Int로 할까 했으나 그냥 Vector2Int로 xz축만을 사용하고 height는 따로 저장하자
//  -이유는 특정 좌표를 찾을때 마우스나 터치를 할텐데 겹쳐있으면 힘들것 같아서
//4. 기즈모로 보여준다.

public class TileManager : MonoBehaviour
{
    #region OnlyForTest
    public Vector2Int startpos = new Vector2Int(0,0);
    public Vector2Int endPos = new Vector2Int(0,0);
    public int moveLimit = 1;
    public int heightLimit = 1;
    public List<Vector2Int> fordebugPath = new List<Vector2Int>();
    #endregion

    #region Properties
    #region Private
    private Dictionary<Vector2Int, Tile> _map = new Dictionary<Vector2Int, Tile>();
    #region ForPathFinding
    private HashSet<Vector2Int> movable = new HashSet<Vector2Int>();
    //up, down, right, left
    private readonly int[] dx = new int[4] { 0, 0, 1, -1 };
    private readonly int[] dy = new int[4] { 1, -1, 0, 0 };
    private bool didInit = false;
    #endregion
    #endregion
    #region Protected
    #endregion
    #region Public
    public Vector3 tileSize = new Vector3(2,1,2);
    public int rows = 1;
    public int cols = 1;
    public Dictionary<Vector2Int, Tile> map
    {
        get { return _map; }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    [ContextMenu("init")]
    private void Initialize()
    {
        //디버깅용
        _map.Clear();



        InitGrid();
        InitHeight();

        didInit = true;


        /*
        //debug
        foreach(Tile node in _map.Values)
        {
            Debug.Log($"x : {node.pos.x}, y : {node.pos.y}, height : {node.height}");
        }
        */
    }

    // 시작지점은 무조건 0,0,0
    private void InitGrid()
    {
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                _map.Add(pos, new Tile(pos, tileSize, 0));
            }
        }
    }

    private void InitHeight()
    {
        //여기서 높이작업
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    public Tile GetTile(Vector3 worldpos)
    {
        Vector2Int coord = new Vector2Int(Mathf.FloorToInt(worldpos.x / tileSize.x), Mathf.FloorToInt(worldpos.z / tileSize.z));
        return GetTile(coord);
    }
    public Tile GetTile(Vector2Int coordinate)
    {
        if (_map.ContainsKey(coordinate))
        {
            return _map[coordinate];
        }
        else
        {
            return null;
        }
    }
    
    public void ShowTiles(HashSet<Vector2Int> tiles)
    {
        //여기에 저 타일들로 이동 가능 영역 보여주기
    }

    /// <summary>
    /// 이동력 안으로 갈수 있는 타일들을 수집
    /// Find tiles which can be accessible in moveLimit
    /// </summary>
    /// <param name="tile">start Tile</param>
    /// <param name="addTile">Function to filter out tiles that cannot be moved</param>
    /// <returns></returns>
    public HashSet<Vector2Int> SearchTile(Tile tile, Func<Tile, Tile, bool> addTile)
    {
        return SearchTile(tile.coordinate, addTile);
    }

    /// <summary>
    /// 이동력 안으로 갈수 있는 타일들을 수집
    /// Find tiles which can be accessible in moveLimit
    /// </summary>
    /// <param name="start">start tile</param>
    /// <param name="addTile">Function to filter out tiles that cannot be moved</param>
    public HashSet<Vector2Int> SearchTile(Vector2Int start, Func<Tile, Tile, bool> addTile)
    {
        movable.Clear();
        if (!_map.ContainsKey(start))
        {
            Debug.LogError("TileManager.SearchTile() wrong start Tile");
        }

        //시작지점 초기화
        _map[start].distance = 0;
        _map[start].prev = start;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(start);
        movable.Add(start);

        while(queue.Count > 0)
        {
            Vector2Int node = queue.Dequeue();
            //상하좌우
            for(int i = 0; i < 4; i++)
            {
                Vector2Int temp = new Vector2Int(node.x + dx[i], node.y + dy[i]);

                if (_map.ContainsKey(temp))
                {
                    if (!addTile(_map[node], _map[temp]))
                    {
                        continue;
                    }

                    int cost = _map[node].distance + _map[temp].cost;

                    //already in the hashset
                    if (movable.Contains(temp))
                    {
                        //if cost is cheaper than before
                        if(cost < _map[temp].distance)
                        {
                            queue.Enqueue(temp);
                            _map[temp].distance = cost;
                            _map[temp].prev = node;
                        }
                    }
                    else
                    {
                        queue.Enqueue(temp);
                        movable.Add(temp);
                        _map[temp].distance = cost;
                        _map[temp].prev = node;
                    }
                }
            }
        }

        //for debug
        foreach(Vector2Int node in movable)
        {
            Debug.Log($"node : {node} prev : {_map[node].prev} cost : {_map[node].distance} center : {_map[node].center}");
        }
        return movable;
    }

    /// <summary>
    /// SearchTile 함수에 기반한 길찾기, 디버그 용으로 쓰고 삭제할 예정
    /// </summary>
    /// <param name="start">시작지점</param>
    /// <param name="end">끝지점</param>
    /// <param name="path">길 순서대로</param>
    /// <returns>성공시 true 실패시 false</returns>
    public bool PathFind(Vector2Int start, Vector2Int end, out List<Vector2Int> path)
    {
        path = new List<Vector2Int>();

        SearchTile(start, (from, to) =>
        {
            return (from.distance + to.cost <= moveLimit) && Math.Abs(from.height - to.height) <= heightLimit;
        });

        if (!movable.Contains(end))
            return false;

        Vector2Int temp = end;
        while(temp != start)
        {
            path.Add(temp);
            temp = _map[temp].prev;
        }

        path.Add(start);
        path.Reverse();

        fordebugPath.Clear();
        fordebugPath = path.ToList();

        //ForDebug
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(path[i]);
        }

        return true;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    //for test
    private void Start()
    {
        Initialize();

        //Debug.Log("TileManager.Start() start");
        //bool temp = false;
        //temp = PathFind(startpos, endPos, out List<Vector2Int> path);
        //if (temp)
        //{
        //    Debug.Log("Success");
        //}
        //Debug.Log("TileManager.Start() End");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!didInit)
            Initialize();

        if (_map == null || _map.Count == 0) return;

        foreach (Tile node in _map.Values)
        {
            if (movable.Contains(node.coordinate))
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            Gizmos.DrawWireCube(node.center, new Vector3(tileSize.x * 0.9f, 0.1f, tileSize.z * 0.9f));
            
        }

        for(int i = 1; i < fordebugPath.Count; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_map[fordebugPath[i - 1]].center, _map[fordebugPath[i]].center);
        }
    }
#endif
    #endregion
}
