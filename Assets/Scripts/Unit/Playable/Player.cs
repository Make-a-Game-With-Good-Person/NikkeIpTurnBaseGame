using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Unit
{
    public EquipInven playerEquip;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerEquip.InitItemValue();
        EquipInit();
    }

    void EquipInit()
    {
        for(int i = 0; i <playerEquip.baseItems.Length; i++)
        {
            this[EStatType.MaxHP] += playerEquip.baseItems[i].baseStat[(int)EStatType.MaxHP];
            this[EStatType.ATK] += playerEquip.baseItems[i].baseStat[(int)EStatType.ATK];
            this[EStatType.DEF] += playerEquip.baseItems[i].baseStat[(int)EStatType.DEF];
            this[EStatType.Accuracy] += playerEquip.baseItems[i].baseStat[(int)EStatType.Accuracy];
            this[EStatType.Avoid] += playerEquip.baseItems[i].baseStat[(int)EStatType.Avoid];
            this[EStatType.CRIMul] += playerEquip.baseItems[i].baseStat[(int)EStatType.CRIMul];
            this[EStatType.Visual] += playerEquip.baseItems[i].baseStat[(int)EStatType.Visual];
            this[EStatType.Jump] = 1; // 당장 값이 없음
            this[EStatType.Move] += playerEquip.baseItems[i].baseStat[(int)EStatType.Move];
        }


        this[EStatType.HP] = this[EStatType.MaxHP];
    }
}
