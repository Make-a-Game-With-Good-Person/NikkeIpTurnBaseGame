using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;

/// <summary>
/// FireStore에서 불러온 유저 데이터를 캐싱해놓는 클래스
/// </summary>
public class UserDataCache
{
    public string Nickname { get; private set; }
    public int Stage { get; private set; }
    public int Credits { get; private set; }
    public int BattleDatas { get; private set; }
    public Dictionary<string, int> Equip { get; private set; } = new Dictionary<string, int>();

    public void SetData(string nickname, int stage, int credits, int battleDatas, Dictionary<string, int> equip)
    {
        Nickname = nickname;
        Stage = stage;
        Credits = credits;
        BattleDatas = battleDatas;
        Equip = new Dictionary<string, int>(equip);
    }

    public void UpdateStageClear(int stageLv, int rewardCredits, int rewardBattleDatas)
    {
        Stage = stageLv;
        Credits += rewardCredits;
        BattleDatas += rewardBattleDatas;
    }

    public void UpdateEquipmentUpgrade(int cost, int battleData, EquipType equipType)
    {
        Credits -= cost;
        BattleDatas -= battleData;

        switch (equipType)
        {
            case EquipType.Helmet:
                Equip["helmetLv"]++;
                break;
            case EquipType.Armor:
                Equip["armorLv"]++;
                break;
            case EquipType.Gloves:
                Equip["glovesLv"]++;
                break;
            case EquipType.Boots:
                Equip["bootsLv"]++;
                break;
            default:
                break;
        }
    }


}
