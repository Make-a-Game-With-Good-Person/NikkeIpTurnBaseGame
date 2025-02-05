using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Properties
    #region Private
    private Stat _target;
    private EquipmentData _equipmentData;
    /// <summary>
    /// 장비 수치
    /// </summary>
    [SerializeField]private float[] _statApply = new float[(int)EStatType.Count];
    /// <summary>
    /// 착용시에 필요한 스텟
    /// </summary>
    [SerializeField]private float[] _statRequire = new float[(int)EStatType.Count];
    #endregion
    #region Protected
    #endregion
    #region Public
    public EquipmentData equipmentData
    {
        get { return _equipmentData; }
        set 
        { 
            _equipmentData = value;
            Initialize();
        }
    }
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    /// <summary>
    /// 멤버변수 초기화 해줄 함수
    /// </summary>
    private void Initialize()
    {
        for (int i = 0; i < _statApply.Length; i++) 
        {
            _statApply[i] = 0.0f;
        }

        //기초
        for (int i = 0; i < _equipmentData.Item_Stat_Value.Length; i++)
        {
            _statApply[(int)_equipmentData.Item_Stat_Value[i].Type] += _equipmentData.Item_Stat_Value[i].Value;
        }
        
        //강화 적용
        for(int i = 0; i < _equipmentData.Item_Level_Status; i++)
        {
            for (int j = 0; j < _equipmentData.Item_Valueup_Resource[i].Value.Length; j++)
            {
                _statApply[(int)_equipmentData.Item_Valueup_Resource[i].Value[j].Type] += _equipmentData.Item_Valueup_Resource[i].Value[j].Value;
            }
        }
    }
    #endregion
    #region Protected
    protected virtual bool CheckRequirement(Stat stat)
    {
        for (int i = 0; i < (int)EStatType.Count; i++)
        {
            if (stat[(EStatType)i] < _statRequire[i])
            {
                return false;
            }
        }
        //요구조건 충족
        return true;
    }
    protected virtual void ApplyEquipment()
    {
        for (int i = 0; i < (int)EStatType.Count; i++)
        {
            _target[(EStatType)i] += _statApply[i];
        }
    }
    protected virtual void UnApplyEquipment()
    {
        for (int i = 0; i < (int)EStatType.Count; i++)
        {
            _target[(EStatType)i] -= _statApply[i];
        }
    }
    #endregion
    #region Public
    public virtual bool Equip(Stat stat)
    {
        if(!CheckRequirement(stat))
        {
            return false;
        }

        _target = stat;
        ApplyEquipment();
        return true;
    }
    public virtual bool UnEquip()
    {
        UnApplyEquipment();
        _target = null;
        return true;
    }
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    protected virtual void Start()
    {
        Initialize();
    }
    protected virtual void OnDestroy()
    {
        UnEquip();
    }
    #endregion

    #region test
    private void OnEnable()
    {
        test();
    }
    private void OnDisable()
    {
        UnEquip();
    }
    public void test()
    {
        Stat stat = GetComponentInParent<Stat>();
        Equip(stat);
    }
    #endregion
}
