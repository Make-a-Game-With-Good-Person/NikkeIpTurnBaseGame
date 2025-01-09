using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
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

    protected BattleManager battleManager;

    private TargetFinder targetFinder;
    private void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        targetFinder = new TargetFinder(battleManager);
    }

    public virtual void Action() {
        Debug.Log("��ų ����!");
    }

    public HashSet<Vector2Int> GetSkillRange()
    {
        HashSet<Vector2Int> skillRange = battleManager.tileManager.SearchTile(battleManager.curControlUnit.tile.coordinate, (from, to) =>
        { return from.distance + 1 <= battleManager.curSelectedSkill.skillRange && Math.Abs(from.height - to.height) <= battleManager.curSelectedSkill.skillHeight; }
        );

        targetFinder.FindTarget(skillRange);

        return skillRange;
    }

}
