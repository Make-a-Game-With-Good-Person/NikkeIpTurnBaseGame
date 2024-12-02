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
    bool _isTurned;

    public bool isTurned
    {
        get
        {
            return _isTurned;
        }
        set
        {
            _isTurned = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _isTurned = true;
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
    #region Anim Events
    #endregion
}
