using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// 각 유닛 스킬들의 베이스가 될 클래스.
/// 이 클래스를 상속받아 Action을 재정의해 스킬 클래스들을 각각 만들면 된다.
/// </summary>
public class UnitSkill : MonoBehaviour, IAction
{
    public Image skillIcon;
    public string skillName;
    public float skillDamage; // 이건 나중에 유닛의 공격력과 따로 처리해서 계산해야함
    public ParticleSystem skillVFX;
    public float skillRange;
    public float skillHeight;
    public int skillLevel;
    public int skillCost;
    public Unit owner;

    public UnityEvent changeStateWhenActEnd;

   
    public virtual void Action() { }

}
