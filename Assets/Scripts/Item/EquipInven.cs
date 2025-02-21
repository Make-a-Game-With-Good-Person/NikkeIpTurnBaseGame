using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipInven : MonoBehaviour
{
    public BaseItem[] baseItems = new BaseItem[4];
    UserDataManager userDataManager = UserDataManager.Instance;

    public void InitItemValue()
    {
        baseItems[(int)EquipType.Helmet].ItemLv = userDataManager.UserData.Equip["helmetLv"];
        baseItems[(int)EquipType.Armor].ItemLv = userDataManager.UserData.Equip["armorLv"];
        baseItems[(int)EquipType.Gloves].ItemLv = userDataManager.UserData.Equip["glovesLv"];
        baseItems[(int)EquipType.Boots].ItemLv = userDataManager.UserData.Equip["bootsLv"];

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
