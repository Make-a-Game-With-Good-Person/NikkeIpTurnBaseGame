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
        Debug.Log($"���� ���ݷ� : {this[EStatType.ATK]}");
        this[EStatType.ATK] += plusPower;
        Debug.Log($"���� �� ���ݷ� : {this[EStatType.ATK]}");
        battleManager.EndBuffEvent.AddListener(ResetPower);
    }

    void ResetPower()
    {
        if (!buffed) return;
        buffed = false;
        this[EStatType.ATK] -= plusPower;
        Debug.Log($"�߰� �� ���ݷ� : {this[EStatType.ATK]}");
    }
}
