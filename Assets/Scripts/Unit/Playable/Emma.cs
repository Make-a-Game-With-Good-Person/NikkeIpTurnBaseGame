using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emma : Player
{
    [SerializeField] float shieldLv;
    public float shield;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shield = shieldLv * this[EStatType.LV];
    }

    public override void TakeDamage(float dmg)
    {
        Debug.Log($"Emma�� {this.gameObject.name} �� {dmg} ��ŭ�� �������� ����");
        if (shield - dmg >= 0f)
        {
            shield -= dmg;
        }
        else
        {
            float remainDmg = Mathf.Abs(shield - dmg);
            shield = 0;
            this[EStatType.HP] -= remainDmg;
            Debug.Log($"���� ���� : {shield}, ���� ü�� : {this[EStatType.HP]}");
        }

        if (this[EStatType.HP] <= 0f)
        {
            //if(animator != null) animator.SetTrigger("Dead");
            OnDead();
        }
        else
        {
            //if(animator != null) animator.SetTrigger("TakeDamage");
        }
    }
}
