using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSkill : UnitSkill
{
    [SerializeField] int coolTime;
    public int curCoolTime = 0;
    bool coolCheck = false;
    LayerMask skillLayerMask;

    void CoolTimeCheck()
    {
        if (!coolCheck) return;
        curCoolTime++;
        if (curCoolTime >= coolTime)
        {
            coolCheck = false;
            curCoolTime = 0;
        }
    }

    protected override void Start()
    {
        base.Start();
        skillLayerMask = (1 << 7) | (1 << 9);
        battleManager.RoundEndEvent.AddListener(CoolTimeCheck);
    }

    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {
        coolCheck = true;
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 " + battleManager.selectedTarget.gameObject.name + "를 향한 공격");
        //battleManager.curControlUnit.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);
        if (IsActionAccuracy())
        {
            Debug.Log("명중");
            if (IsActionCritical())
            {
                Debug.Log("크리티컬!");
                battleManager.selectedTarget.GetComponent<IDamage>().TakeDamage(
                    battleManager.curControlUnit[EStatType.ATK] * battleManager.curControlUnit[EStatType.CRIMul]);
            }
            else
            {
                battleManager.selectedTarget.GetComponent<IDamage>().TakeDamage(battleManager.curControlUnit[EStatType.ATK]);
            }
        }
        yield return new WaitForSeconds(2f);
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 공격 끝");
        changeStateWhenActEnd?.Invoke();
    }

    IEnumerator CheckAttackTarget()
    {
        yield return null;

        Collider[] targets = Physics.OverlapSphere(this.transform.position, skillTargetRange, skillLayerMask);

        foreach (Collider target in targets)
        {
            if (IsActionAccuracy())
            {
                Debug.Log("명중");
                if (IsActionCritical())
                {
                    Debug.Log("크리티컬!");
                    target.GetComponent<IDamage>().TakeDamage(battleManager.curControlUnit[EStatType.ATK] * battleManager.curControlUnit[EStatType.CRIMul]);
                }
                else
                {
                    target.GetComponent<IDamage>().TakeDamage(battleManager.curControlUnit[EStatType.ATK]);
                }
            }
        }
    }

}
