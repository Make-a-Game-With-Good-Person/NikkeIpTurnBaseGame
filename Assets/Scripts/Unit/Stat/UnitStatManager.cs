using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

public class UnitStatManager
{
    public Dictionary<int, StatStructure> dicStatComponent;

    public StatStructure LoadStat(int index)
    {
        var StatTable = Resources.Load<TextAsset>("Json/Nikke_Unit_DataTable").text;

        var arrStatTable = JsonConvert.DeserializeObject<StatStructure[]>(StatTable);

        this.dicStatComponent = arrStatTable.ToDictionary(x => x.unit_index);

        return dicStatComponent[index];
    }
}
