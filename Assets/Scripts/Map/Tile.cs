using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject content = null;
    #region ForPathFinding
    [HideInInspector]public int distance = 0;
    [HideInInspector]public Vector2Int prev;
    #endregion
    #endregion
    #region Events
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

        tileState = TileState.Walkable | TileState.Selectable;
    }
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    #endregion
}