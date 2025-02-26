using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.UI.GridLayoutGroup;

/// <summary>
/// Unit 게임 오브젝트에 붙어있다고 가정
/// 애니메이션만 건드리고 Unit을 움직이거나 하는건 UnitMovement에서
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

    #region Animation Parameter
    private int B_Run = Animator.StringToHash("B_Run");
    private int B_Cover = Animator.StringToHash("B_Cover");
    private int B_isFullCover = Animator.StringToHash("B_isFullCover");
    private int I_Peeking = Animator.StringToHash("I_Peeking");
    private int I_AttackType = Animator.StringToHash("I_AttackType");
    private int T_Attack = Animator.StringToHash("T_Attack");
    #endregion

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
    #endregion
    #region Protected
    #endregion
    #region Public
    public void Cover()
    {
        _anim.SetBool(B_Cover, true);
    }
    public void UnCover()
    {
        _anim.SetBool(B_Cover, false);
    }

    public void SetFullCover(bool isfullCover)
    {
        _anim.SetBool(B_isFullCover, isfullCover);
    }
    public void SetPeekingType(int type)
    {
        _anim.SetInteger(I_Peeking, type);
    }

    /// <summary>
    /// 공격할때 
    /// </summary>
    /// <param name="attackAnimType"></param>
    /// <param name="isPeeking"></param>
    public void Attack(int attackAnimType, bool isPeeking)
    {
        _anim.SetTrigger(T_Attack);
    }

    public void StartRunning()
    {
        _anim.SetBool(B_Run, true);
    }
    public void EndRunning()
    {
        _anim.SetBool(B_Run, false);
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
