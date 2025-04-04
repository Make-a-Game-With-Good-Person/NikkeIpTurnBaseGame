using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillVesti : UnitSkill
{
    LayerMask skillLayerMask;
    protected override void Start()
    {
        base.Start();

        skillLayerMask = (1 << 8) | (1 << 9);
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

        Collider[] targets = Physics.OverlapSphere(battleManager.selectedTarget.transform.position, skillTargetRange, skillLayerMask);

        if (attackVFX != null) 
            Instantiate(attackVFX, new Vector3(battleManager.selectedTarget.transform.position.x, 
                battleManager.selectedTarget.transform.position.y + 1f, 
                battleManager.selectedTarget.transform.position.z), 
                Quaternion.identity);

        foreach (Collider target in targets)
        {
            if (IsActionAccuracy())
            {
                float dmg = CalculAttackDamage();
                Debug.Log("명중");
                if (target.gameObject.layer == LayerMask.NameToLayer("Cover")) // 벽에 맞을 때
                {
                    if (IsActionCritical())
                    {
                        Debug.Log("크리티컬!");
                        target.GetComponent<IDamage>().TakeDamage(dmg * battleManager.curControlUnit[EStatType.CRIMul]*1.2f);
                    }
                    else
                    {
                        target.GetComponent<IDamage>().TakeDamage(dmg * 1.2f);
                    }
                }
                else // 적이 맞을 때
                {
                    if (IsActionCritical())
                    {
                        Debug.Log("크리티컬!");
                        target.GetComponent<IDamage>().TakeDamage(dmg * battleManager.curControlUnit[EStatType.CRIMul] * 0.8f);
                    }
                    else
                    {
                        target.GetComponent<IDamage>().TakeDamage(dmg * 0.8f);
                    }
                }
                
                
            }
        }
    }
}
