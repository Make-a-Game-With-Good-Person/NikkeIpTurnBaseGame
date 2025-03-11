using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInven : MonoBehaviour
{
    public BaseItem[] baseItems = new BaseItem[4];
    UserDataManager userDataManager;

    public void InitItemValue()
    {
        userDataManager = UserDataManager.Instance;

        baseItems[0].ItemLv = userDataManager.UserData.EquipLevel[EquipType.helmet.ToString()];
        baseItems[1].ItemLv = userDataManager.UserData.EquipLevel[EquipType.armor.ToString()];
        baseItems[2].ItemLv = userDataManager.UserData.EquipLevel[EquipType.gloves.ToString()];
        baseItems[3].ItemLv = userDataManager.UserData.EquipLevel[EquipType.boots.ToString()];

        SetItemValue();
    }

    void SetItemValue()
    {
        for(int i = 0; i < baseItems.Length; i++)
        {
            for(int stat = 0; stat < baseItems[i].baseStat.Length; stat++)
            {
                baseItems[i].baseStat[stat] *= baseItems[i].ItemLv;
            }
        }
    }
}
