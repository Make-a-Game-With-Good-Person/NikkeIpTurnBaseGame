using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// Unit ���� ������Ʈ�� �پ��ִٰ� ����
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
    /// <param name="direction">��ŷ�� ����</param>
    /// <returns>-1 : ���买 ����, 0 : ���� ����, 1: ���买 ����, 2: ���买 ������</returns>
    private int CheckPeeking(EDirection direction)
    {
        if (!_owner.tile.halfCovers[(int)direction] && !_owner.tile.fullCovers[(int)direction])
        {
            return -1;
        }

        for (int i = 0; i < 2; i++)
        {
            int dir = ((int)direction + 2 + i) % 4;

            Debug.Log($"for�� ���� i�� {i}, direction : {direction}, dir : {(EDirection)dir}");

            //1. ���� �˻�, �ٷο� Ÿ�Ϸ� ���� ���⿡ ������ ��� ������ �� ������
            if (!_owner.tile.halfCovers[dir] && !_owner.tile.fullCovers[dir])
            {
                Vector2Int closeTile = _owner.tile.coordinate + new Vector2Int(dx[dir], dy[dir]);

                Debug.Log($"1�� if�� ���, owner.tile = {_owner.tile.coordinate}, closeTile = {closeTile}");

                //2. ���� �˻�, �ٷ� �� Ÿ���� ���̰� ������
                if (_owner.tile.height == _battleManager.tileManager.map[closeTile].height)
                {
                    //3. �밢�� �˻�, ���̿� ���� �˻縦 �Ѵ� �ؾ��Ұ� -> ���̴� ������ ������ �ڵ����� �ذ�
                    //��Ÿ�Ͽ��� �밢�� ���� Ÿ������ ���� ������ ������
                    if (!_battleManager.tileManager.map[closeTile].halfCovers[(int)direction] && !_battleManager.tileManager.map[closeTile].fullCovers[(int)direction])
                    {
                        switch (direction)
                        {
                            case EDirection.Front:
                            case EDirection.Back:
                                //������
                                if (i == 0)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking ������");
                                    return 2;
                                }
                                //����
                                else if (i == 1)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking ����");
                                    return 1;
                                }
                                //for�� ���� ����
                                i = 2;
                                break;
                            case EDirection.Right:
                            case EDirection.Left:
                                //����
                                if (i == 0)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking ����");
                                    return 1;
                                }
                                //������
                                else if (i == 1)
                                {
                                    Debug.Log("UnitAnimationController.CheckPeeking ������");
                                    return 2;
                                }
                                //for�� ���� ����
                                i = 2;
                                break;
                        }
                    }
                }
            }
        }

        //�Ѵ� �ȵ����� �׳� �߽ɿ��� ��ŷ
        Debug.Log("UnitAnimationController.CheckPeeking ���");
        return 0;
    }
    /// <returns>-1 : ���买 ����, 0 : ���� ����, 1: ���买 ����, 2: ���买 ������</returns>
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
    /// �����Ҷ� 
    /// </summary>
    /// <param name="attackAnimType"></param>
    /// <param name="isPeeking"></param>
    public void Attack(int attackAnimType, bool isPeeking)
    {
        //���� �ΰ� ���ļ� ��Ե� �ٸ� �ִϸ��̼� ���
        //_anim.SetTrigger(Attack + attackAnimType);

        //���� �����ؾ��ϴ� �������� �ƴ��� �Ǵ��� isPeeking���� �˾Ƴ�
        int peekingType = -1;
        if (isPeeking)
        {
            Debug.Log("if�� ����");
            Tile tile = _battleManager.tileManager.GetTile(_battleManager.selectedTarget.transform.position);

            //Unit ���� ������Ʈ�� ������Ʈ�� �پ������Ƿ�
            EDirection direction = _battleManager.tileManager.GetDirection_B_to_A(tile.coordinate ,_owner.tile.coordinate);
            //������, ���� �˻� -> (EDirection + 2) % 4, (EDirection + 3) %4 �ϸ� ���� Ȥ�� �������� ���´ٴ� ������ �̿�

            peekingType = CheckPeeking(direction);
        }
        else
        {
            peekingType = -1;
        }

        //�Ѵ� �ȵ����� �׳� �߽ɿ���, �ƴϸ� �ƿ� ��ŷ�� ���ϴ� ��쿡

        //���� �ΰ� ���ļ� ��Ե� �ٸ� �ִϸ��̼� ���
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
