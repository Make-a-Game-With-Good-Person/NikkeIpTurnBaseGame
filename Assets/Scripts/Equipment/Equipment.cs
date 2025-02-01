using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Properties
    #region Private
    private Stat _target;
    private int _upgradeLevel;
    /// <summary>
    /// 1���� �ö� ������ ��ġ, ����� Serialize�� �ϵ��� �ϰ� ���߿� Init�Լ��� �ٲܰ�
    /// </summary>
    [SerializeField]private float[] _statPerUpgrade = new float[(int)EStatType.Count];
    [SerializeField]private float[] _statApply = new float[(int)EStatType.Count];
    /// <summary>
    /// ����ÿ� �ʿ��� ����
    /// </summary>
    [SerializeField]private float[] _statRequire = new float[(int)EStatType.Count];
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #region Events
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    /// <summary>
    /// ������� �ʱ�ȭ ���� �Լ�
    /// </summary>
    private void Initialize()
    {

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
        //�䱸���� ����
        return true;
    }
    protected virtual void ApplyEquipment()
    {
        for (int i = 0; i < (int)EStatType.Count; i++)
        {
            _target[(EStatType)i] += _statApply[i] + _statPerUpgrade[i] * _upgradeLevel;
        }
    }
    protected virtual void UnApplyEquipment()
    {
        for (int i = 0; i < (int)EStatType.Count; i++)
        {
            _target[(EStatType)i] -= _statApply[i] + _statPerUpgrade[i] * _upgradeLevel;
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
