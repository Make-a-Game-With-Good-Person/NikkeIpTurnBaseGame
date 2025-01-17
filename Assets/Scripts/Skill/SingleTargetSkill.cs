using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetSkill : UnitSkill
{
    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {
        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // 이건 스킬마다 바인딩해놓은 유닛을 타겟으로 잡는거
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함
        // 애니메이터 설정 anim.SetTrigger(블라블라);
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

        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // 이건 스킬마다 바인딩해놓은 유닛을 타겟으로 잡는거
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 공격 끝");
        changeStateWhenActEnd?.Invoke();
    }
}
