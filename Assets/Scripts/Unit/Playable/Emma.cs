using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emma : Unit
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
        Debug.Log($"{this.gameObject.name} 이 {dmg} 만큼의 데미지를 받음");
        if (shield - dmg >= 0f)
        {
            shield -= dmg;
        }
        else
        {
            float remainDmg = Mathf.Abs(shield - dmg);
            shield = 0;
            this[EStatType.HP] -= remainDmg;
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
