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
    public int BattleData { get; private set; }
    public Dictionary<string, int> EquipLevel { get; private set; } = new Dictionary<string, int>();

    public void SetData(string nickname, int stage, int credits, int battleData, Dictionary<string, int> equip)
    {
        Nickname = nickname;
        Stage = stage;
        Credits = credits;
        BattleData = battleData;
        EquipLevel = new Dictionary<string, int>(equip);
    }

    public void UpdateStageClear(int stageLv, int rewardCredits, int rewardBattleData)
    {
        Stage = stageLv;
        Credits += rewardCredits;
        BattleData += rewardBattleData;
    }

    public void UpdateEquipmentUpgrade(int cost, int battleData, EquipType equipType)
    {
        Credits -= cost;
        BattleData -= battleData;
        EquipLevel[equipType.ToString()]++;
    }


}
