using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Eunhwa : Player
{
    bool buffed;
    [SerializeField] float plusPower;
    protected override void Start()
    {
        base.Start();
        buffed = true;
        Debug.Log($"기존 공격력 : {this[EStatType.ATK]}");
        this[EStatType.ATK] += plusPower;
        Debug.Log($"은신 중 공격력 : {this[EStatType.ATK]}");
        battleManager.EndBuffEvent.AddListener(ResetPower);
    }

    void ResetPower()
    {
        if (!buffed) return;
        buffed = false;
        this[EStatType.ATK] -= plusPower;
        Debug.Log($"발각 후 공격력 : {this[EStatType.ATK]}");
    }
}
