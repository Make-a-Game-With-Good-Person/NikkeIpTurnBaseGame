using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTargetSkill : UnitSkill
{
    LayerMask skillLayerMask;
    protected override void Start()
    {
        base.Start();

        skillLayerMask = (1 << 7) | (1 << 8) | (1 << 9);
    }


    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {
        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // 이건 스킬마다 바인딩해놓은 유닛을 타겟으로 잡는거
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 " + battleManager.selectedTarget.gameObject.name + "를 향한 공격");
        //battleManager.curControlUnit.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);
        //skillVFX.Play();

        yield return StartCoroutine(CheckAttackTarget());
        yield return new WaitForSeconds(2f);

        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // 이건 스킬마다 바인딩해놓은 유닛을 타겟으로 잡는거
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 공격 끝");
        changeStateWhenActEnd?.Invoke();
    }

    IEnumerator CheckAttackTarget()
    {
        yield return null;

        Collider[] targets = Physics.OverlapSphere(this.transform.position, skillTargetRange, skillLayerMask);

        foreach(Collider target in targets){
            if (IsActionAccuracy())
            {
                float dmg = CalculAttackDamage();
                Debug.Log("명중");
                if (IsActionCritical())
                {
                    Debug.Log("크리티컬!");
                    target.GetComponent<IDamage>().TakeDamage(dmg * battleManager.curControlUnit[EStatType.CRIMul]);
                }
                else
                {
                    target.GetComponent<IDamage>().TakeDamage(dmg);
                }
            }
        }
    }
}
