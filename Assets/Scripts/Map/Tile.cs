using System;
using UnityEngine;
using UnityEngine.Events;

public class Tile
{
    #region Properties
    #region Private
    private Vector2Int _coordinate;
    private Vector3 _worldPos;
    private Vector3 _center;
    private Vector3 _size;
    private int _height;
    #endregion
    #region Protected
    #endregion
    #region Public
    /// <summary>
    /// 타일의 상태
    /// Status of this Tile
    /// </summary>
    public TileState tileState
    {
        get; protected set;
    }
    /// <summary>
    /// 타일 좌표
    /// Coordinate of this Tile
    /// </summary>
    public Vector2Int coordinate
    {
        get { return _coordinate; }
        private set { _coordinate = value; }
    }
    /// <summary>
    /// 실제로 위치하는 월드 포지션, 기준은 직육면체 윗면의 왼쪽아래 코너
    /// Actual world position, base is left down corner of the top of the box
    /// </summary>
    public Vector3 worldPos
    {
        get { return _worldPos; }
        private set { _worldPos = value; }
    }
    /// <summary>
    /// 타일의 중심의 월드 포지션, 기준은 직육면체 윗면의 중심
    /// world position of center of tile, base is center of the top of the box
    /// </summary>
    public Vector3 center
    {
        get { return _center; }
        private set { _center = value; }
    }
    /// <summary>
    /// 타일 하나의 사이즈
    /// size of tile
    /// </summary>
    public Vector3 size
    {
        get { return _size; }
        private set { _size = value; }
    }
    /// <summary>
    /// 타일의 높이
    /// Height of this Tile
    /// </summary>
    public int height
    {
        get { return _height; }
        set 
        {
            _height = value;
            Vector3 plusHeight = new Vector3(0, size.y * _height, 0);
            worldPos = worldPos + plusHeight;
            center = center + plusHeight;
        }
    }
    /// <summary>
    /// 이 타일을 지나가기 위한 이동력 수치(진흙으로 덮혀있으면 2가 드는 방식)
    /// the cost which is required for pass this tile
    /// </summary>
    public int cost = 1;
    /// <summary>
    /// 타일 위에 있는 물체
    /// GameObject on the this Tile
    /// </summary>
    public Unit content
    {
        get; protected set;
    }

    /// <summary>
    /// EDirection enum을 사용할것
    /// </summary>
    public Cover[] covers = new Cover[4];
    public bool[] fullCovers = new bool[4] { false, false, false, false };
    public bool[] halfCovers = new bool[4] { false, false, false, false };
    public bool[] ladders = new bool[4] { false, false, false, false};
    #region ForPathFinding
    [HideInInspector]public int distance = 0;
    [HideInInspector]public Vector2Int prev;
    #endregion
    #endregion
    #region Events
    /// <summary>
    /// 이벤트 발생은 PassTile()함수를 이용하고 이건 이벤트 등록과 제거에만 활용할것
    /// </summary>
    public UnityEvent<Unit> passTileEvent = new UnityEvent<Unit>();
    #endregion
    #endregion

    #region Constructor
    /// <summary>
    /// 타일 하나 생성
    /// Make a tile
    /// </summary>
    /// <param name="coordinate">Coordinate of this tile</param>
    /// <param name="size">size of one tile</param>
    /// <param name="height">Height of this tile</param>
    public Tile(Vector2Int coordinate, Vector3 size, int height)
    {
        this.coordinate = coordinate;
        this.size = size;
        this.height = height;

        worldPos = new Vector3(size.x * this.coordinate.x, size.y * this.height, size.z * this.coordinate.y);
        center = worldPos + new Vector3(this.size.x / 2, 0, this.size.z / 2);

        tileState = TileState.Walkable | TileState.Selectable | TileState.Placeable;
    }
    #endregion

    #region Methods
    #region Private
    /// <summary>
    /// https://stackoverflow.com/questions/3746274/line-intersection-with-aabb-rectangle
    /// 코헨 서덜랜드 클리핑 알고리즘 변형
    /// </summary>
    /// <param name="a1">직선 1의 한점</param>
    /// <param name="a2">직선 1의 다른 한점</param>
    /// <param name="b1">직선 2의 한점</param>
    /// <param name="b2">직선 2의 다른 한점</param>
    /// <param name="intersection">직선1과 2의 만나는 점</param>
    /// <returns></returns>
    private bool Intersects(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
    {
        intersection = Vector2.zero;

        Vector2 b = a2 - a1;
        Vector2 d = b2 - b1;
        float bDotDPerp = Vector2.Dot(b, d);

        // if b dot d == 0, it means the lines are parallel so have infinite intersection points
        if (bDotDPerp == 0)
            return false;

        Vector2 c = b1 - a1;
        float t = Vector2.Dot(c, d) / bDotDPerp;
        if (t < 0 || t > 1)
            return false;

        float u = Vector2.Dot(c, b) / bDotDPerp;
        if (u < 0 || u > 1)
            return false;

        intersection = a1 + t * b;

        return true;
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    public override string ToString()
    {
        return string.Format("Coordinate : {0}, Height : {1}", this.coordinate.ToString(), this.height.ToString());
    }

    public bool Place(Unit content)
    {
        if (this.content != null)
        {
            return false;
        }

        this.content = content;
        content.tile = this;
        tileState = tileState | TileState.Placed;
        return true;
    }
    public bool UnPlace(Unit unit)
    {
        if (content == unit)
        {
            content.tile = null;
            content = null;
            tileState = tileState ^ TileState.Placed;

            return true;
        }

        return false;
    }

    public void PassTile(Unit passUnit)
    {
        passTileEvent?.Invoke(passUnit);
    }
    
    /// <summary>
    /// 아직 제대로 안만들었다. 디버깅 용도로 Walkable을 빼는 용도
    /// </summary>
    public void SetObstacle()
    {
        tileState = tileState ^ TileState.Placeable ^ TileState.Walkable;
    }

    //a1, a2는 직선의 점 2개
    public bool CollisionDetect(Vector2 a1, Vector2 a2)
    {
        Vector2[] b = {
            new Vector2(worldPos.x, worldPos.z),
            new Vector2(worldPos.x + size.x, worldPos.z),
            new Vector2(worldPos.x, worldPos.z + size.z),
            new Vector2(worldPos.x + size.x, worldPos.z + size.z)
        };

        for(int i = 0; i < 4; i++)
        {
            if(Intersects(a1, a2, b[i], b[(i + 1) %4], out Vector2 intersection))
            {
                return true;
            }
        }

        return false;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}