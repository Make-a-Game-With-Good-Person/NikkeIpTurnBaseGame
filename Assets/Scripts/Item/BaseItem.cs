using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseItem : ScriptableObject
{
    public Sprite ItemIcon;
    public string ItemName;
    public string ItemExplainText;

    public EquipType EquipType;
    public int ItemLv;
    public float[] baseStat = new float[(int)EStatType.Count];

}
