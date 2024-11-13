using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour, IDamage
{
    [SerializeField] Transform destroyVFX;
    bool isComplete;
    int index;
    int hp;
    int maxHp;
    int def;
    int dmg;
    
    /*private void Init(CoverSpec _coverSpec)
    {
        index = _coverSpec.obstacle_index;
        maxHp = _coverSpec.obstacle_hp;
        def = _coverSpec.obstacle_defence;
        dmg = _coverSpec.obstacle_damage;

        hp = maxHp;
    }*/
    // Start is called before the first frame update
    void OnEnable()
    {
        //Init();
    }

    public void TakeDamage(int dmg)
    {
        if (isComplete) return;

        hp -= dmg;
        if(hp <= 0)
        {
            Instantiate(destroyVFX, this.transform.position, Quaternion.identity, null);
            Destroy(this.gameObject);
        }
    }
    
}
