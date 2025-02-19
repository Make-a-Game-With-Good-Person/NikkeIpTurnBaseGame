using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// Unit 게임 오브젝트에 붙어있다고 가정
/// </summary>
public class UnitAnimationController : MonoBehaviour
{
    #region Properties
    #region Private
    private Unit _owner;
    private Animator _anim;
    private BattleManager _battleManager;
    //Front, Back, Right, Left
    private readonly int[] dx = new int[4] { 0, 0, 1, -1 };
    private readonly int[] dy = new int[4] { 1, -1, 0, 0 };
    /// <summary>
    /// 0: fornt, 1: Back. 2: Right, 3: Left, etc : none cover
    /// </summary>
    private int _coverDirection = -1;
    #endregion
    #region Protected
    #endregion
    #region public
    #endregion
    #region Events
    public UnityEvent AnimEndEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    /// <param name="direction">피킹할 방향</param>
    /// <returns>-1 : 엄페물 없음, 0 : 엄폐물 위로, 1: 엄페물 왼쪽, 2: 엄페물 오른쪽</returns>
    private int CheckPeeking(EDirection direction)
    {
        if (!_owner.tile.halfCovers[(int)direction] && !_owner.tile.fullCovers[(int)direction])
        {
            return -1;
        }

        for (int i = 0; i < 2; i++)
        {
            int dir = ((int)direction + 2 + i) % 4;

            Debug.Log($"for문 시작 i는 {i}, direction : {direction}, dir : {(EDirection)dir}");

            //1. 엄폐물 검사, 바로옆 타일로 가는 방향에 엄폐물이 없어서 지나갈 수 있을것
            if (!_owner.tile.halfCovers[dir] && !_owner.tile.fullCovers[dir])
            {
                Vector2Int closeTile = _owner.tile.coordinate + new Vector2Int(dx[dir], dy[dir]);

                Debug.Log($"1번 if문 통과, owner.tile = {_owner.tile.coordinate}, closeTile = {closeTile}");

                //2. 높이 검사, 바로 옆 타일의 높이가 같을것
                if (_owner.tile.height == _battleManager.tileManager.map[closeTile].height)
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
                                    Debug.Log("UnitAnimationController.CheckPeeking 오른쪽");
                                    return 2;
                                }
                                //왼쪽
                                else if (i == 1)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking 왼쪽");
                                    return 1;
                                }
                                //for문 강제 종료
                                i = 2;
                                break;
                            case EDirection.Right:
                            case EDirection.Left:
                                //왼쪽
                                if (i == 0)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking 왼쪽");
                                    return 1;
                                }
                                //오른쪽
                                else if (i == 1)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking 오른쪽");
                                    return 2;
                                }
                                //for문 강제 종료
                                i = 2;
                                break;
                        }
                    }
                }
            }
        }

        //둘다 안됐으면 그냥 중심에서 피킹
        Debug.Log("UnitAnimationController.CheckPeeking 가운데");
        return 0;
    }
    /// <returns>-1 : 엄페물 없음, 0 : 엄폐물 위로, 1: 엄페물 왼쪽, 2: 엄페물 오른쪽</returns>
    private int CheckPeeking(int direction)
    {
        return CheckPeeking((EDirection)direction);
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    public void Cover()
    {

    }

    /// <summary>
    /// 공격할때 
    /// </summary>
    /// <param name="attackAnimType"></param>
    /// <param name="isPeeking"></param>
    public void Attack(int attackAnimType, bool isPeeking)
    {
        //대충 두개 합쳐서 어떻게든 다른 애니메이션 출력
        //_anim.SetTrigger(Attack + attackAnimType);

        //이제 빼꼼해야하는 공격인지 아닌지 판단을 isPeeking으로 알아냄
        int peekingType = -1;
        if (isPeeking)
        {
            Debug.Log("if문 시작");
            Tile tile = _battleManager.tileManager.GetTile(_battleManager.selectedTarget.transform.position);

            //Unit 게임 오브젝트에 컴포넌트로 붙어있으므로
            EDirection direction = _battleManager.tileManager.GetDirection_B_to_A(tile.coordinate ,_owner.tile.coordinate);
            //오른쪽, 왼쪽 검사 -> (EDirection + 2) % 4, (EDirection + 3) %4 하면 왼쪽 혹은 오른쪽이 나온다는 성질을 이용

            peekingType = CheckPeeking(direction);
        }
        else
        {
            peekingType = -1;
        }

        //둘다 안됐으면 그냥 중심에서, 아니면 아예 피킹을 안하는 경우에

        //대충 두개 합쳐서 어떻게든 다른 애니메이션 출력
        //_anim.SetTrigger(Attack + attackAnimType);
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _owner = GetComponentInParent<Unit>();
        _anim = GetComponent<Animator>();
        _battleManager = FindObjectOfType<BattleManager>();
    }
    #endregion
}
