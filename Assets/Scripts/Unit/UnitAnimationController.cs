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
    #endregion
    #region Protected
    #endregion
    #region Public
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
        if (isPeeking)
        {
            Debug.Log("if�� ����");
            Tile tile = _battleManager.tileManager.GetTile(_battleManager.selectedTarget.transform.position);

            //Unit ���� ������Ʈ�� ������Ʈ�� �پ������Ƿ�
            EDirection direction = _battleManager.tileManager.GetDirection_B_to_A(tile.coordinate ,_owner.tile.coordinate);
            //������, ���� �˻� -> (EDirection + 2) % 4, (EDirection + 3) %4 �ϸ� ���� Ȥ�� �������� ���´ٴ� ������ �̿�
            
            for(int i = 0; i < 2; i++)
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
                                    if(i == 0)
                                    {
                                        Debug.Log("������");
                                    }
                                    //����
                                    else if(i == 1)
                                    {
                                        Debug.Log("����");
                                    }
                                    //for�� ���� ����
                                    i = 2;
                                    break;
                                case EDirection.Right:
                                case EDirection.Left:
                                    //����
                                    if (i == 0)
                                    {
                                        Debug.Log("����");
                                    }
                                    //������
                                    else if (i == 1)
                                    {
                                        Debug.Log("������");
                                    }
                                    //for�� ���� ����
                                    i = 2;
                                    break;
                            }
                        }
                    }
                }
            }

            //�Ѵ� �ȵ����� �׳� �߽ɿ���
        }

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
        _owner = GetComponent<Unit>();
        _anim = GetComponentInChildren<Animator>();
        _battleManager = FindObjectOfType<BattleManager>();
    }
    #endregion
}
