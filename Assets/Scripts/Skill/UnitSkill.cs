using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
/// �� ���� ��ų���� ���̽��� �� Ŭ����.
/// �� Ŭ������ ��ӹ޾� Action�� �������� ��ų Ŭ�������� ���� ����� �ȴ�.
/// </summary>
public class UnitSkill : MonoBehaviour, IAction
{
    public Image skillIcon;
    public string skillName;
    public float skillDamage; // �̰� ���߿� ������ ���ݷ°� ���� ó���ؼ� ����ؾ���
    public ParticleSystem skillVFX;
    public float skillRange;
    public float skillHeight;
    public int skillLevel;
    public int skillCost;
    public Unit owner;

    public UnityEvent changeStateWhenActEnd;

   
    public virtual void Action() { }

}
