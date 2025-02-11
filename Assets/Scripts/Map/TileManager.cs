using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

//�޸�
//1. ������ 0, 0, 0�� ����
//2. ��ĭ ũ�⸦ Vector3�� �Է�, y���� ���̷� ���
//3. map���� �������� Vector3Int�� �ұ� ������ �׳� Vector2Int�� xz�ุ�� ����ϰ� height�� ���� ��������
//  -������ Ư�� ��ǥ�� ã���� ���콺�� ��ġ�� ���ٵ� ���������� ����� ���Ƽ�
//4. ������ �����ش�.

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
    //Front, Back, Right, Left
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

    public LayerMask tileObjectMask;
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
        //������
        _map.Clear();

        InitGrid();
        InitHeight();
        InitCover();
        didInit = true;


        //Debug.Log($"coordi 3,4 {map[new Vector2Int(3, 4)].halfCovers[(int)EDirection.Front]}");
        //Debug.Log($"coordi 3,4 {map[new Vector2Int(3, 4)].fullCovers[(int)EDirection.Front]}");
        /*
        //debug
        foreach(Tile node in _map.Values)
        {
            Debug.Log($"x : {node.pos.x}, y : {node.pos.y}, height : {node.height}");
        }
        */
    }

    // ���������� ������ 0,0,0
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
        //���⼭ �����۾�
        foreach(Tile tile in map.Values)
        {
            //100 ���̿��� �Ʒ��� ���� ��, 100 ���̴ϱ� ���̵� 100
            RaycastHit[] hits = Physics.RaycastAll(tile.center + (Vector3.up * 100), Vector3.down, 100.0f, tileObjectMask);

            foreach(RaycastHit hit in hits)
            {
                TileObject tileObject = hit.collider.GetComponentInParent<TileObject>();
                if (tileObject != null) 
                {
                    tile.height = (int)(hit.point.y / tileSize.y);
                    if (!tileObject.isWalkable)
                    {
                        tile.SetObstacle();
                    }
                }
            }
        }
    }
    private void InitCover()
    {
        foreach(Tile tile in map.Values)
        {
            //��, ��, ��, �� ������
            Vector3[] rayStart = new Vector3[4];
            rayStart[(int)EDirection.Front] = tile.worldPos + new Vector3(tileSize.x * 0.5f, 100, tileSize.z);
            rayStart[(int)EDirection.Back] = tile.worldPos + new Vector3(tileSize.x * 0.5f, 100, 0);
            rayStart[(int)EDirection.Right] = tile.worldPos + new Vector3(tileSize.x, 100, tileSize.z * 0.5f);
            rayStart[(int)EDirection.Left] = tile.worldPos + new Vector3(0, 100, tileSize.z * 0.5f);

            for(int i = 0; i < 4; i++)
            {
                //��ü ���� �޸鿡�� ���̰� ������ �ְ� �ϴ� �ɼ�
                RaycastHit[] hits = Physics.RaycastAll(rayStart[i], Vector3.down, 100, tileObjectMask);
                RaycastHit hit = default;

                Cover cover = null;
                TileObject tileObject = null;

                //�ƹ� ������ ���� �ȵǾ����� �׳� continue �ؼ� hit.distance�� 0�̶� �״�� ����Ǵ°� ����
                //Ŀ���� ������ �ȵǸ� �׳� ��������
                foreach(RaycastHit hitinfo in hits)
                {
                    if (hitinfo.collider != null)
                    {
                        cover = hitinfo.collider.GetComponentInParent<Cover>();
                        tileObject = hitinfo.collider.GetComponentInParent<TileObject>();
                        //Debug.Log($"{tile.coordinate}, {(EDirection)i}, {hitinfo.collider.gameObject.name}");

                        if(tileObject != null)
                        {
                            hit = hitinfo;
                            break;
                        }
                    }
                }

                if(tileObject == null)
                {
                    continue;
                }

                //������ ������������ ���̰� tileSize.y�� 2�� ��� ����
                //���� ������ ���������� ���� -> fullCover, 0�̶�� �ϸ� �� �׷��� �ణ�� ������ ��
                if (hit.point.y > tile.worldPos.y + 1.9f * tileSize.y)
                {
                    tile.fullCovers[i] = true;

                    if (cover != null)
                    {
                        tile.covers[i] = cover;
                    }
                }
                //HalfCover, 0.5f * (tileSize.y * 2) = 1.0f * tileSize.y ������ �ణ�� ������ �� 
                else if(hit.point.y > tile.worldPos.y + 0.95f * tileSize.y)
                {
                    tile.halfCovers[i] = true;

                    if (cover != null)
                    {
                        tile.covers[i] = cover;
                    }
                }
            }
        }
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

    public int GetDistanceBetweenTiles(Vector2Int first, Vector2Int sec)
    {
        if (map.ContainsKey(first) && map.ContainsKey(sec))
        {
            return Math.Abs(first.x - sec.x) + Math.Abs(first.y - sec.y);
        }
        else return -1;
    }

    public EDirection GetDirection_B_to_A(Vector2Int A, Vector2Int B)
    {
        EDirection direction = EDirection.Front;

        Vector2Int AtoB = B - A;
        int min = 100;
        for(int i = 0; i < 4; i++)
        {
            int dot = dx[i] * AtoB.x + dy[i] * AtoB.y;

            if(dot < min)
            {
                direction = (EDirection)i;
            }
        }

        return direction;
    }

    public void ShowTiles(HashSet<Vector2Int> tiles)
    {
        //���⿡ �� Ÿ�ϵ�� �̵� ���� ���� �����ֱ�
    }

    /// <summary>
    /// �̵��� ������ ���� �ִ� Ÿ�ϵ��� ����
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
    /// �̵��� ������ ���� �ִ� Ÿ�ϵ��� ����
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

        //�������� �ʱ�ȭ
        _map[start].distance = 0;
        _map[start].prev = start;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(start);
        movable.Add(start);

        while(queue.Count > 0)
        {
            Vector2Int node = queue.Dequeue();
            //�����¿�
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

        /*
        //for debug
        foreach(Vector2Int node in movable)
        {
            Debug.Log($"node : {node} prev : {_map[node].prev} cost : {_map[node].distance} center : {_map[node].center}");
        }
        */
        return movable;
    }

    /// <summary>
    /// SearchTile �Լ��� ����� ��ã��, ����� ������ ���� ������ ����
    /// </summary>
    /// <param name="start">��������</param>
    /// <param name="end">������</param>
    /// <param name="path">�� �������</param>
    /// <returns>������ true ���н� false</returns>
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
            else if((node.tileState & TileState.Walkable) > 0)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.red;
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
