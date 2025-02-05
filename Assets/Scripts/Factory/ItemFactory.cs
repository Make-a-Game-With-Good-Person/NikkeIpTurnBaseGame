using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

public struct EquipmentData
{
    public int Item_Index;
    public int Item_Level_Status;
    public int Item_Level_Status_Require;
    public ValueUpData[] Item_Valueup_Resource;
    public StatData[] Item_Stat_Value;
}

public struct ValueUpData
{
    public string Name;
    public StatData[] Value;
    public int Count;
}

public struct StatData
{
    public EStatType Type;
    public int Value;
}

public class ItemFactory
{
    public GameObject Create(int index)
    {
        GameObject obj = null;
        EquipmentData data = default;
        string json = null;

        switch (index / 10000)
        {
            case 1:
                json = Resources.Load<TextAsset>("Json/Weapon_DataTable").text;
                break;
            case 2:
                json = Resources.Load<TextAsset>("Json/Armor_DataTable").text;
                break;
            default:
                json = null;
                break;
        }
        var arrDatas = JsonConvert.DeserializeObject<EquipmentData[]>(json);
        foreach (var Data in arrDatas)
        {
            if (Data.Item_Index == index)
            {
                data = Data;
                break;
            }
        }

        if(data.Item_Index != index)
        {
            return null;
        }
        obj = new GameObject(index.ToString());

        Equipment equip = obj.AddComponent<Equipment>();
        equip.equipmentData = data;

        return obj;
    }
}
