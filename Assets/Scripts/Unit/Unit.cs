using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stat , IDamage
{
    [HideInInspector] public Tile tile;
    [SerializeField] Animator _animator;
    public EUnitState unitState; // 적 유닛이 사용할 enum, 나중에 Enemy클래스를 분리할 일이 있으면 그쪽으로 넘기면 됨
    public List<UnitSkill> unitSkills;
    public int index;
    public Transform shoulder;
    public Transform rayPointer; // 레이 발사 지점
    public Transform rayEnter; // 레이가 들어오는 지점
    bool _attackable; // 공격 가능 여부
    bool _movable; // 이동 가능 여부

    public bool attack_Re; // 턴 대기(종료) 버튼을 누르고 만약 다시 턴을 돌아와 이어가고 싶다면 이 변수에 있는 값을 이용해 여부를 결정함
    public bool move_Re; // 이것도 마찬가지

    public Animator animator
    {
        get
        {
            return _animator;
        }
    }
    public bool attackable
    {
        get
        {
            return _attackable;
        }
        set
        {
            _attackable = value;
        }
    }

    public bool movable
    {
        get
        {
            return _movable;
        }
        set
        {
            _movable = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetAble();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log($"{this.gameObject.name} 이 {dmg} 만큼의 데미지를 받음");
        this[EStatType.HP] -= dmg;
        if (this[EStatType.HP] <= 0f) Destroy(this);
    }

    public void ResetAble()
    {
        _attackable = true;
        attack_Re = true;
        _movable = true;
        move_Re = true;
    }
    #region Anim Events
    #endregion
}
