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
    private Dictionary<Vector2Int, Tile> map = new Dictionary<Vector2Int, Tile>();
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
        map.Clear();



        InitGrid();
        InitHeight();

        didInit = true;


        /*
        //debug
        foreach(Tile node in map.Values)
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
                map.Add(pos, new Tile(pos, tileSize, 0));
            }
        }
    }

    private void InitHeight()
    {
        //���⼭ �����۾�
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
        if (map.ContainsKey(coordinate))
        {
            return map[coordinate];
        }
        else
        {
            return null;
        }
    }
    
    public void ShowTiles(HashSet<Vector2Int> tiles)
    {
        //���⿡ �� Ÿ�ϵ�� �̵� ���� ���� �����ֱ�
    }

    /// <summary>
    /// �̵��� ������ ���� �ִ� Ÿ�ϵ��� ����
    /// Find tiles which can be accessible in moveLimit
    /// </summary>
    /// <param name="start">start tile</param>
    /// <param name="moveLimit">distance from start tile</param>
    public HashSet<Vector2Int> SearchTile(Vector2Int start, int moveLimit, int heightLimit)
    {
        movable.Clear();
        if (!map.ContainsKey(start))
        {
            Debug.LogError("TileManager.SearchTile() wrong start Tile");
        }

        //�������� �ʱ�ȭ
        map[start].distance = 0;
        map[start].prev = start;
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

                if (map.ContainsKey(temp))
                {
                    int cost = map[node].distance + map[temp].cost;
                    if (cost > moveLimit)
                        continue;

                    if(Math.Abs(map[node].height - map[temp].height) > heightLimit)
                        continue;

                    //already in the hashset
                    if (movable.Contains(temp))
                    {
                        //if cost is cheaper than before
                        if(cost < map[temp].distance)
                        {
                            queue.Enqueue(temp);
                            map[temp].distance = cost;
                            map[temp].prev = node;
                        }
                    }
                    else
                    {
                        queue.Enqueue(temp);
                        movable.Add(temp);
                        map[temp].distance = cost;
                        map[temp].prev = node;
                    }
                }
            }
        }

        //for debug
        foreach(Vector2Int node in movable)
        {
            Debug.Log($"node : {node} prev : {map[node].prev} cost : {map[node].distance} center : {map[node].center}");
        }
        return movable;
    }
    /// <summary>
    /// SearchTile �Լ��� ����� ��ã��
    /// </summary>
    /// <param name="start">��������</param>
    /// <param name="end">������</param>
    /// <param name="path">�� �������</param>
    /// <returns>������ true ���н� false</returns>
    public bool PathFind(Vector2Int start, Vector2Int end, out List<Vector2Int> path)
    {
        path = new List<Vector2Int>();

        SearchTile(start, moveLimit, heightLimit);

        if (!movable.Contains(end))
            return false;

        Vector2Int temp = end;
        while(temp != start)
        {
            path.Add(temp);
            temp = map[temp].prev;
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
        Debug.Log("TileManager.Start() start");
        Initialize();
        bool temp = false;
        temp = PathFind(startpos, endPos, out List<Vector2Int> path);
        if (temp)
        {
            Debug.Log("Success");
        }
        Debug.Log("TileManager.Start() End");
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!didInit)
            Initialize();

        if (map == null || map.Count == 0) return;

        foreach (Tile node in map.Values)
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
            Gizmos.DrawLine(map[fordebugPath[i - 1]].center, map[fordebugPath[i]].center);
        }
    }
#endif
    #endregion
}
