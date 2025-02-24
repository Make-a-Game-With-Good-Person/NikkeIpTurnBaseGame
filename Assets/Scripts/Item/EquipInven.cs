using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInven : MonoBehaviour
{
    public BaseItem[] baseItems = new BaseItem[4];
    UserDataManager userDataManager = UserDataManager.Instance;

    public void InitItemValue()
    {
        int index = 0;
        foreach (EquipType Etype in Enum.GetValues(typeof(EquipType)))
        {
            string key = Etype.ToString();
            baseItems[index].ItemLv = userDataManager.UserData.EquipLevel[key];
            index++;
        }

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
