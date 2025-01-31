using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stat , IDamage
{
    [HideInInspector] public Tile tile;
    [SerializeField] Animator _animator;
    [Header("������ ���� �ε���"), Space(.5f)]
    [SerializeField] int unit_index;
    BattleManager owner;

    public EUnitState unitState; // �� ������ ����� enum, ���߿� EnemyŬ������ �и��� ���� ������ �������� �ѱ�� ��
    public List<UnitSkill> unitSkills;
    public Transform shoulder;
    public Transform rayPointer; // ���� �߻� ����
    public Transform rayEnter; // ���̰� ������ ����
    bool _attackable; // ���� ���� ����
    bool _movable; // �̵� ���� ����
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
    /// ������ ������ ������ ������ �� �ʱ�ȭ�ϴ� �Լ�
    /// ����(25.01.16)���� �⺻ Json�����͸� �о�� ����������
    /// ���� ��� ������ �߰��� ��� �� �����Ϳ� �߰��� ���� �������� �ݿ��ž���
    /// </summary>
    /// <param name="index"></param>
    void StatInit(int index)
    {
        StatStructure unitStat = _unitStatManager.LoadStat(index);
        this[EStatType.LV] = unitStat.unit_level;
        this[EStatType.MaxHP] = unitStat.unit_hp * 10000; // �ӽ÷� �ڿ� ���� ���װ�
        this[EStatType.HP] = this[EStatType.MaxHP];
        this[EStatType.ATK] = unitStat.unit_attackValue;
        this[EStatType.DEF] = unitStat.unit_defenceValue;
        this[EStatType.Accuracy] = unitStat.unit_accuracyValue;
        this[EStatType.Avoid] = unitStat.unit_avoidValue;
        this[EStatType.CRIMul] = unitStat.unit_criticalMultiplier;
        this[EStatType.Visual] = unitStat.unit_visualRange;
        this[EStatType.Jump] = 1; // ���� ���� ����
        this[EStatType.Move] = unitStat.unit_moveRange;
        
        Debug.Log("���� �ʱ�ȭ �Ϸ�");
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log($"{this.gameObject.name} �� {dmg} ��ŭ�� �������� ����");
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
        Debug.Log($"{this.gameObject.name}�� ������");
    }
    #region Anim Events
    #endregion
}
