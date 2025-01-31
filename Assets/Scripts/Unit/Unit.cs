using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stat , IDamage
{
    [HideInInspector] public Tile tile;
    [SerializeField] Animator _animator;
    [Header("유닛의 고유 인덱스"), Space(.5f)]
    [SerializeField] int unit_index;
    BattleManager owner;

    public EUnitState unitState; // 적 유닛이 사용할 enum, 나중에 Enemy클래스를 분리할 일이 있으면 그쪽으로 넘기면 됨
    public List<UnitSkill> unitSkills;
    public Transform shoulder;
    public Transform rayPointer; // 레이 발사 지점
    public Transform rayEnter; // 레이가 들어오는 지점
    bool _attackable; // 공격 가능 여부
    bool _movable; // 이동 가능 여부
    public UnitType unitType;

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

    public int index
    {
        get
        {
            return unit_index;
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ResetAble();
        StatInit(unit_index);
        owner = FindObjectOfType<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 유닛의 스텟을 게임을 시작할 때 초기화하는 함수
    /// 현재(25.01.16)까진 기본 Json데이터를 읽어와 적용하지만
    /// 추후 장비나 레벨이 추가될 경우 이 데이터에 추가로 값이 더해져서 반영돼야함
    /// </summary>
    /// <param name="index"></param>
    void StatInit(int index)
    {
        StatStructure unitStat = _unitStatManager.LoadStat(index);
        this[EStatType.LV] = unitStat.unit_level;
        this[EStatType.MaxHP] = unitStat.unit_hp * 10000; // 임시로 뒤에 곱함 안죽게
        this[EStatType.HP] = this[EStatType.MaxHP];
        this[EStatType.ATK] = unitStat.unit_attackValue;
        this[EStatType.DEF] = unitStat.unit_defenceValue;
        this[EStatType.Accuracy] = unitStat.unit_accuracyValue;
        this[EStatType.Avoid] = unitStat.unit_avoidValue;
        this[EStatType.CRIMul] = unitStat.unit_criticalMultiplier;
        this[EStatType.Visual] = unitStat.unit_visualRange;
        this[EStatType.Jump] = 1; // 당장 값이 없음
        this[EStatType.Move] = unitStat.unit_moveRange;
        
        Debug.Log("스텟 초기화 완료");
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log($"{this.gameObject.name} 이 {dmg} 만큼의 데미지를 받음");
        this[EStatType.HP] -= dmg;
        
        if (this[EStatType.HP] <= 0f)
        {
            //if(animator != null) animator.SetTrigger("Dead");
            OnDead();
        }
        else
        {
            //if(animator != null) animator.SetTrigger("TakeDamage");
        }
    }

    public virtual void ResetAble()
    {
        _attackable = true;
        _movable = true;
    }

    private void OnDead()
    {
        owner.OnUnitDead(this);
    }

    private void OnDestroy()
    {
        Debug.Log($"{this.gameObject.name}이 삭제됨");
    }
    #region Anim Events
    #endregion
}
