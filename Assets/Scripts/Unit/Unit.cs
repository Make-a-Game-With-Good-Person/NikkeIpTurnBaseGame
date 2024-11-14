using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Stat , IDamage
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        this[EStatType.HP] -= dmg;
        if (this[EStatType.HP] <= 0f) Destroy(this);
    }
    #region Anim Events
    #endregion
}
