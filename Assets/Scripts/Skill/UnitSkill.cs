using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
/// <summary>
/// 각 유닛 스킬들의 베이스가 될 클래스.
/// 이 클래스를 상속받아 Action을 재정의해 스킬 클래스들을 각각 만들면 된다.
/// </summary>
public class UnitSkill : MonoBehaviour, IAction
{
    [SerializeField] protected Transform skillTargetRangeVFX;
    [SerializeField] protected float skillTargetRange;

    public Image skillIcon;
    public string skillName;
    public float skillDamage; // 이건 나중에 유닛의 공격력과 따로 처리해서 계산해야함
    public ParticleSystem skillVFX;
    public float skillRange;
    public float skillHeight;
    public int skillLevel;
    public int skillCost;
    public Unit owner;
    public LayerMask skillTargetLayerMask;
    public bool isBuffSkill = false;

    public UnityEvent changeStateWhenActEnd;

    protected BattleManager battleManager;

    public TargetFinder targetFinder;
    private BattleCalculator battleCalculator;
    protected virtual void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        targetFinder = new TargetFinder(battleManager);
        battleCalculator = FindObjectOfType<BattleCalculator>();

        SetSkillRangeVFX();
    }

    protected bool IsActionAccuracy()
    {
        return battleCalculator.CheckAccuracy(battleManager.curControlUnit, battleManager.selectedTarget.GetComponent<Unit>());
    }

    protected bool IsActionCritical()
    {
        return battleCalculator.CheckCritical(battleManager.curControlUnit, battleManager.selectedTarget.GetComponent<Unit>());
    }

    protected float CalculAttackDamage()
    {
        return battleCalculator.CalculDamage(battleManager.curControlUnit, battleManager.selectedTarget.GetComponent<Unit>());
    }

    public virtual void Action() {
        Debug.Log("스킬 실행!");
        TurnOffTargetRange();
    }

    public HashSet<Vector2Int> GetSkillRange()
    {
        HashSet<Vector2Int> skillRange = battleManager.tileManager.SearchTile(battleManager.curControlUnit.tile.coordinate, (from, to) =>
        { return from.distance + 1 <= battleManager.curSelectedSkill.skillRange * battleManager.curControlUnit[EStatType.Visual] && 
            Math.Abs(from.height - to.height) <= battleManager.curSelectedSkill.skillHeight * battleManager.curControlUnit[EStatType.Visual]; }
        );

        //targetFinder.FindTarget(skillRange);

        return skillRange;
    }

    public virtual void TurnOnTargetRange()
    {
        Vector3 pos = battleManager.selectedTarget.transform.position;

        skillTargetRangeVFX.gameObject.SetActive(true);
        skillTargetRangeVFX.position = pos;
    }

    public void TurnOffTargetRange()
    {
        skillTargetRangeVFX.gameObject.SetActive(false);
    }

    protected virtual void SetSkillRangeVFX()
    {
        this.skillTargetRangeVFX.transform.localScale = new Vector3(skillTargetRange, 1, skillTargetRange);
        skillTargetRangeVFX.gameObject.SetActive(false);
    }


}
