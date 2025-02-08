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
    [SerializeField] Transform skillTargetRangeVFX;
    [SerializeField] protected float skillTargetRange;

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

    public TargetFinder targetFinder;
    private BattleCalculator battleCalculator;
    protected virtual void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        targetFinder = new TargetFinder(battleManager);
        battleCalculator = FindObjectOfType<BattleCalculator>();

        skillTargetRangeVFX.transform.localScale = new Vector3(skillTargetRange, 1, skillTargetRange);
        skillTargetRangeVFX.gameObject.SetActive(false);
    }

    protected bool IsActionAccuracy()
    {
        return battleCalculator.CheckAccuracy(battleManager.curControlUnit, battleManager.selectedTarget.GetComponent<Unit>());
    }

    protected bool IsActionCritical()
    {
        return battleCalculator.CheckCritical(battleManager.curControlUnit, battleManager.selectedTarget.GetComponent<Unit>());
    }

    public virtual void Action() {
        Debug.Log("��ų ����!");
    }

    public HashSet<Vector2Int> GetSkillRange()
    {
        HashSet<Vector2Int> skillRange = battleManager.tileManager.SearchTile(battleManager.curControlUnit.tile.coordinate, (from, to) =>
        { return from.distance + 1 <= battleManager.curSelectedSkill.skillRange * battleManager.curControlUnit[EStatType.Visual] && 
            Math.Abs(from.height - to.height) <= battleManager.curSelectedSkill.skillHeight * battleManager.curControlUnit[EStatType.Visual]; }
        );

        targetFinder.FindTarget(skillRange);

        return skillRange;
    }

    public void TurnOnTargetRange()
    {
        Vector3 pos = battleManager.selectedTarget.transform.position;

        skillTargetRangeVFX.gameObject.SetActive(true);
        skillTargetRangeVFX.position = pos;
    }

    public void TurnOffTargetRange()
    {
        skillTargetRangeVFX.gameObject.SetActive(false);
    }



}
