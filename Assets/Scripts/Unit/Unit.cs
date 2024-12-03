using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stat , IDamage
{
    [HideInInspector] public Tile tile;
    [SerializeField] Animator animator;
    public List<UnitSkill> unitSkills;
    public int index;
    public Transform shoulder;
    bool _attackable;
    bool _movable;

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
        this[EStatType.HP] -= dmg;
        if (this[EStatType.HP] <= 0f) Destroy(this);
    }

    public void ResetAble()
    {
        _attackable = true;
        _movable = true;
    }
    #region Anim Events
    #endregion
}
