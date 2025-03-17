using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
/// <summary>
/// Jumping의 속도부분을 나중에 바꿀것
/// </summary>
public class UnitMovement : MonoBehaviour
{
    #region Properties
    #region Private
    //Front, Back, Right, Left
    private readonly int[] dx = new int[4] { 0, 0, 1, -1 };
    private readonly int[] dy = new int[4] { 1, -1, 0, 0 };
    private Coroutine _turning;
    #endregion
    #region Protected
    protected BattleManager _battleManager;
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
    //브레젠험 알고리즘을 이용한 지나가는 타일 검출
    private List<Vector2Int> GetExpandedLine(Vector2Int start, Vector2Int end, TileManager map)
    {
        List<Vector2Int> tiles = new List<Vector2Int>();

        //왼쪽아래 꼭짓점
        tiles.AddRange(GetBresenhamLine(start, end, map));
        //왼쪽 위 꼭짓점
        tiles.AddRange(GetBresenhamLine(start + Vector2Int.right, end + Vector2Int.right, map));
        //오른쪽 위 꼭짓점
        tiles.AddRange(GetBresenhamLine(start + Vector2Int.one, end + Vector2Int.one, map));
        //오른쪽 아래 꼭짓점
        tiles.AddRange(GetBresenhamLine(start + Vector2Int.up, end + Vector2Int.up, map));

        tiles.Add(start);

        //중복 제거
        return tiles.Distinct().ToList();
    }

    //브레젠험 알고리즘
    //인터넷에서 가지고 옴
    private List<Vector2Int> GetBresenhamLine(Vector2Int start, Vector2Int end, TileManager map)
    {
        List<Vector2Int> line = new List<Vector2Int>();

        int x = start.x;
        int y = start.y;
        int dx = Math.Abs(end.x - start.x);
        int dy = Math.Abs(end.y - start.y);
        int sx = (start.x < end.x) ? 1 : -1;
        int sy = (start.y < end.y) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            line.Add(new Vector2Int(x, y));

            if (x == end.x && y == end.y) break;

            int e2 = 2 * err;
            if (e2 >= -dy)
            {
                err -= dy;
                x += sx;
            }
            if (e2 <= dx)
            {
                err += dx;
                y += sy;
            }
        }

        line.Remove(start);

        return line;
    }

    /// <param name="direction">피킹할 방향</param>
    /// <returns>-1 : 엄페물 없음, 0 : 엄폐물 위로, 1: 엄페물 왼쪽, 2: 엄페물 오른쪽</returns>
    private int CheckPeeking(EDirection direction)
    {
        if (!unit.tile.halfCovers[(int)direction] && !unit.tile.fullCovers[(int)direction])
        {
            return -1;
        }

        for (int i = 0; i < 2; i++)
        {
            int dir = ((int)direction + 2 + i) % 4;

            Debug.Log($"for문 시작 i는 {i}, direction : {direction}, dir : {(EDirection)dir}");

            //1. 엄폐물 검사, 바로옆 타일로 가는 방향에 엄폐물이 없어서 지나갈 수 있을것
            if (!unit.tile.halfCovers[dir] && !unit.tile.fullCovers[dir])
            {
                Vector2Int closeTile = unit.tile.coordinate + new Vector2Int(dx[dir], dy[dir]);

                Debug.Log($"1번 if문 통과, owner.tile = {unit.tile.coordinate}, closeTile = {closeTile}");

                //2. 높이 검사, 바로 옆 타일의 높이가 같을것
                if (unit.tile.height == _battleManager.tileManager.map[closeTile].height)
                {
                    //3. 대각선 검사, 높이와 엄폐물 검사를 둘다 해야할것 -> 높이는 엄폐물이 없으면 자동으로 해결
                    //옆타일에서 대각선 방향 타일으로 가는 엄폐물이 없을것
                    if (!_battleManager.tileManager.map[closeTile].halfCovers[(int)direction] && !_battleManager.tileManager.map[closeTile].fullCovers[(int)direction])
                    {
                        switch (direction)
                        {
                            case EDirection.Front:
                            case EDirection.Back:
                                //오른쪽
                                if (i == 0)
                                {
                                    Debug.Log("UnitMovement.CheckPeeking 오른쪽");
                                    return 2;
                                }
                                //왼쪽
                                else if (i == 1)
                                {
                                    Debug.Log("UnitMovement.CheckPeeking 왼쪽");
                                    return 1;
                                }
                                break;
                            case EDirection.Right:
                            case EDirection.Left:
                                //왼쪽
                                if (i == 0)
                                {
                                    Debug.Log("UnitMovement.CheckPeeking 왼쪽");
                                    return 1;
                                }
                                //오른쪽
                                else if (i == 1)
                                {
                                    Debug.Log("UnitMovement.CheckPeeking 오른쪽");
                                    return 2;
                                }
                                break;
                        }
                    }
                }
            }
        }

        //둘다 안됐으면 기본적으로 오른쪽에 엄폐하는 걸로 디폴트
        //엄폐물이 있다는건 여기까지 온것으로 가늠할수 있음
        Debug.Log("UnitMovement.CheckPeeking 가운데");
        return 2;
    }
    /// <returns>-1 : 엄페물 없음, 0 : 엄폐물 위로, 1: 엄페물 왼쪽, 2: 엄페물 오른쪽</returns>
    private int CheckPeeking(int direction)
    {
        return CheckPeeking((EDirection)direction);
    }
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
        bool isOpponent = false;
        if(to.content != null)
        {
            //유닛 타입이 같으면 0보다 크다
            isOpponent = ((unit.unitType & to.content.unitType) <= 0);
        }

        return (from.distance + to.cost <= unit[EStatType.Move]) &&
            (Math.Abs(from.height - to.height) <= unit[EStatType.Jump]) &&
            ((to.tileState & TileState.Walkable) > 0) &&
            !isOpponent;
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

    protected virtual void Cover()
    {
        //엄폐물 방향으로 돌림, Cover 애니메이션 출력
        int checkDirection = -1; //엄폐할 방향
        int peekingDirection = -1;  //피킹 방향
        //앞, 뒤, 우, 좌 순서로 체크
        for(int i = 0; i < 4; i++)
        {
            peekingDirection = CheckPeeking(i);
            if(peekingDirection != -1)
            {
                checkDirection = i;
                break;
            }
        }

        //엄폐가 가능할때
        if(checkDirection != -1)
        {
            //돌려
            if (_turning != null)
            {
                StopCoroutine(_turning);
            }
            _turning = StartCoroutine(Turning(
                _battleManager.tileManager.map
                [unit.tile.coordinate + new Vector2Int(dx[checkDirection], dy[checkDirection])]
                ));

            unit.myAnim?.SetFullCover(unit.tile.fullCovers[checkDirection]);
            unit.myAnim?.SetPeekingType(peekingDirection);

            unit.myAnim?.Cover();
        }
    }
    protected virtual void UnCover()
    {
        unit.myAnim?.UnCover();
    }

    protected virtual void EndAttack()
    {
        unit.myAnim.EndAttack();
        Cover();
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
            Debug.LogError("UnitMovement.Traverse(), can`t find path, wrong destination");
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

        //엄폐물에서 벗어나는 애니메이션
        UnCover();

        #region 순차접근 길찾기 방식
        /*
        //대각선 계산을 위한 리스트
        List<Vector2Int> temp = new List<Vector2Int>();
        //start 지점 추가
        temp.Add(tile);

        for (int i = 1; i < path.Count; i++)
        {
            int height = Math.Clamp(map.map[path[i - 1]].height - map.map[path[i]].height, -1, 1);

            switch (height) 
            {
                case -1:
                    {
                        if(temp.Count > 1)
                        {
                            yield return StartCoroutine(Turning(map.map[temp.Last()]));
                            yield return StartCoroutine(Running(map.map[temp.Last()], map));
                        }

                        yield return StartCoroutine(Turning(map.map[path[i]]));
                        yield return StartCoroutine(Climbing(map.map[path[i]], map));
                        temp.Clear();
                        temp.Add(path[i]);
                    }
                    break;
                case 0:
                    {
                        //대각선 판정
                        Vector2Int min = new Vector2Int(Math.Min(temp[0].x, path[i].x), Math.Min(temp[0].y, path[i].y));
                        Vector2Int max = new Vector2Int(Math.Max(temp[0].x, path[i].x), Math.Max(temp[0].y, path[i].y));

                        bool isdiagonal = true;
                        for (int x = min.x; x <= max.x && isdiagonal; x++)
                        {
                            for(int y = min.y; y <= max.y && isdiagonal; y++)
                            {
                                Vector2Int coord = new Vector2Int(x, y);

                                //갈수 있는 타일이 아님, 시작지점은 range에 포함이 안되어있으므로 예외처리
                                if (!range.Contains(coord) && coord != path[0])
                                {
                                    isdiagonal = false;
                                    break;
                                }
                            }
                        }

                        Debug.Log($"UnitMovement.Traverse() isdiagonal = {isdiagonal}, temp.Count = {temp.Count}, next coord = {path[i]}");

                        if (isdiagonal)
                        {
                            temp.Add(path[i]);
                        }
                        else
                        {
                            Vector2Int last = temp.Last();
                            if (temp.Count > 1)
                            {
                                yield return StartCoroutine(Turning(map.map[last]));
                                yield return StartCoroutine(Running(map.map[last], map));
                            }
                            temp.Clear();
                            temp.Add(last);
                            temp.Add(path[i]);
                        }
                    }
                    break;
                case 1:
                    {
                        if (temp.Count > 1)
                        {
                            yield return StartCoroutine(Turning(map.map[temp.Last()]));
                            yield return StartCoroutine(Running(map.map[temp.Last()], map));
                        }
                        yield return StartCoroutine(Turning(map.map[path[i]]));
                        yield return StartCoroutine(Jumping(map.map[path[i - 1]], map.map[path[i]], map));
                        temp.Clear();
                        temp.Add(path[i]);
                    }
                    break;
            }
        }

        //Debug.Log($"UnitMovement.Traverse() temp.Count = {temp.Count}, next coord = {temp.Last()}");

        //height == 0이 계속되고 전부 대각선 판정이 나서 추가만 하면서 움직이지는 않았을 경우
        if (temp.Count > 1)
        {
            yield return StartCoroutine(Turning(map.map[temp.Last()]));
            yield return StartCoroutine(Running(map.map[temp.Last()], map));
        }
        */
        #endregion

        #region 역순접근 길찾기 방식
        //위의 순차 방식이 아닌 역순 처리 방식으로 처리
        //지금까지 계산된 path의 인덱스
        int calIndex = 0;
        while (calIndex != path.Count - 1)
        {
            for (int i = path.Count - 1; i > calIndex; i--)
            {
                Debug.Log($"UnitMovement.Traverse() reversePathFinding path[calIndex] = {path[calIndex]}, path[i] = {path[i]}");

                int height = Math.Clamp(map.map[path[calIndex]].height - map.map[path[i]].height, -1, 1);
                switch (height)
                {
                    case -1:
                        {
                            //i가 calIndex의 + 1인덱스, 즉 마지막
                            if(i == calIndex + 1)
                            {
                                calIndex = i;
                                if(_turning != null)
                                {
                                    StopCoroutine(_turning);
                                }
                                _turning = StartCoroutine(Turning(map.map[path[calIndex]]));
                                yield return StartCoroutine(Climbing(map.map[path[calIndex - 1]], map.map[path[calIndex]], map));
                            }
                        }
                        break;
                    case 0:
                        {
                            //i가 calIndex의 + 1인덱스, 즉 마지막
                            //코드는 같지만 여러 함수들을 거치지 않아도 되는 예외상황이므로 계산을 최소화 하려는 의도
                            if (i == calIndex + 1)
                            {
                                calIndex = i;
                                if (_turning != null)
                                {
                                    StopCoroutine(_turning);
                                }
                                _turning = StartCoroutine(Turning(map.map[path[calIndex]]));
                                yield return StartCoroutine(Running(map.map[path[calIndex]], map));
                                break;
                            }

                            List<Vector2Int> tiles = GetExpandedLine(path[calIndex], path[i],map);

                            bool check = true;

                            //x,y를 길이로 가지는 직사각형
                            for(int x = Math.Min(path[calIndex].x, path[i].x); x <= Math.Max(path[calIndex].x, path[i].x); x++)
                            {
                                for(int y = Math.Min(path[calIndex].y, path[i].y); y <= Math.Max(path[calIndex].y, path[i].y); y++)
                                {
                                    Vector2Int node = new Vector2Int(x, y);
                                    //그 직사각형이 직선그린것에 겹칠때만 계산
                                    if (tiles.Contains(node))
                                    {
                                        //높이가 같지 않거나 갈 수 없는 위치라면
                                        if (map.map[node].height != map.map[path[calIndex]].height || (!range.Contains(node) && node != path[0]))
                                        {
                                            check = false;
                                            break;
                                        }
                                    }
                                }
                            }

                            //갈수 있는 경우
                            if (check)
                            {
                                calIndex = i;
                                if (_turning != null)
                                {
                                    StopCoroutine(_turning);
                                }
                                _turning = StartCoroutine(Turning(map.map[path[calIndex]]));
                                yield return StartCoroutine(Running(map.map[path[calIndex]], map));
                            }
                        }
                        break;
                    case 1:
                        {
                            //i가 calIndex의 + 1인덱스, 즉 마지막
                            if (i == calIndex + 1)
                            {
                                calIndex = i;
                                if (_turning != null)
                                {
                                    StopCoroutine(_turning);
                                }
                                _turning = StartCoroutine(Turning(map.map[path[calIndex]]));
                                yield return StartCoroutine(Jumping(map.map[path[calIndex - 1]], map.map[path[calIndex]], map));
                            }
                        }
                        break;
                }
            }
        }

        #endregion

        map.map[path[path.Count - 1]].Place(unit);

        //엄폐하는 애니메이션 재생 함수
        Cover();
    }

    protected IEnumerator Running(Tile to, TileManager map)
    {
        Vector3 dir = to.center - unit.gameObject.transform.position;
        float dist = dir.magnitude;
        float delta = 0;
        dir.Normalize();

        //여기에 애니메이션 파라미터 수정
        unit.myAnim?.StartRunning();

        //일단 한칸한칸이니까
        HashSet<Tile> passedTile = new HashSet<Tile>();

        while (dist > 0)
        {
            Tile tile = map.GetTile(unit.transform.position);

            if (!passedTile.Contains(tile)) 
            {
                tile.PassTile(unit);
                passedTile.Add(tile);
            }

            delta = 10.0f * Time.deltaTime;

            if(delta > dist)
            {
                delta = dist;
            }

            unit.gameObject.transform.Translate(delta * dir, Space.World);
            dist -= delta;

            yield return null;
        }

        //달리기 애니메이션 중지
        unit.myAnim?.EndRunning();

        passedTile.Clear();
    }

    //아래로 점프
    protected IEnumerator Jumping(Tile from, Tile to, TileManager map)
    {
        //수평 이동용
        Vector3 dir = to.center - unit.gameObject.transform.position;
        dir.y = 0;
        float dist = dir.magnitude;
        float delta = 0;
        dir.Normalize();

        //수직 이동용
        //1.0을 나중에 이동속도로 바꿀것
        float t2 = dist / 1.0f;
        float t1 = 0;
        float maxHeight = Mathf.Max(from.height, to.height) + map.tileSize.y;

        while (dist > 0)
        {
            t1 += Time.deltaTime;
            float val = t1 / t2;
            //수평 이동
            delta = 10.0f * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }

            unit.gameObject.transform.Translate(delta * dir, Space.World);
            dist -= delta;

            //수직 이동
            float height = 0;
            if (val <= 0.5f)
            {
                // 위로 이동
                val *= 2;
                height = -1 * (maxHeight - from.height) * val * (val - 2) + from.height;
            }
            else
            {
                // 아래로 이동
                val = 2 * (1.0f - val);
                height = -1 * (maxHeight - to.height) * val * (val - 2) + to.height;
            }

            unit.transform.position = new Vector3(unit.transform.position.x, height, unit.transform.position.z);

            yield return null;
        }
        unit.transform.position = to.center;
    }

    //위로 기어 올라감
    protected IEnumerator Climbing(Tile from ,Tile to, TileManager map)
    {
        yield return StartCoroutine(Jumping(from,to,map));
    }

    protected IEnumerator Turning(Tile to)
    {
        Vector3 dir = to.center - unit.transform.position;
        dir.y = 0;
        

        float angle = Vector3.Angle(unit.transform.forward, dir);
        float right = Vector3.Dot(unit.transform.right, dir) < 0 ? -1f : 1f;
        float delta = 0;

        while(angle > 0)
        {
            delta = 360.0f * Time.deltaTime;

            if(delta > angle)
            {
                delta = angle;
            }

            unit.transform.Rotate(delta * right * Vector3.up, Space.World);
            angle -= delta;
            yield return null;
        }
    }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        unit = GetComponent<Unit>();
        _battleManager = FindObjectOfType<BattleManager>();
        foreach(UnitSkill skill in unit.unitSkills)
        {
            skill.changeStateWhenActEnd.AddListener(EndAttack);
        }
    }
    #endregion
}