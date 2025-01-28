using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : UnitSkill
{
    [SerializeField] protected int coolTime;
    public int curCoolTime = 0;
    public bool coolCheck = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    public void CoolTimeCheck()
    {
        Debug.Log("ÄðÅ¸ÀÓ Ã¼Å©<<<<<<<<<<<<<");
        if (!coolCheck) return;
        curCoolTime++;
        if (curCoolTime >= coolTime)
        {
            coolCheck = false;
            curCoolTime = 0;
        }
    }

}
