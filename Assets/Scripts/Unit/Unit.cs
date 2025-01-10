using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stat , IDamage
{
    [HideInInspector] public Tile tile;
    [SerializeField] Animator _animator;
    public EUnitState unitState; // �� ������ ����� enum, ���߿� EnemyŬ������ �и��� ���� ������ �������� �ѱ�� ��
    public List<UnitSkill> unitSkills;
    public int index;
    public Transform shoulder;
    public Transform rayPointer; // ���� �߻� ����
    public Transform rayEnter; // ���̰� ������ ����
    bool _attackable; // ���� ���� ����
    bool _movable; // �̵� ���� ����

    public bool attack_Re; // �� ���(����) ��ư�� ������ ���� �ٽ� ���� ���ƿ� �̾�� �ʹٸ� �� ������ �ִ� ���� �̿��� ���θ� ������
    public bool move_Re; // �̰͵� ��������

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
        Debug.Log($"{this.gameObject.name} �� {dmg} ��ŭ�� �������� ����");
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
