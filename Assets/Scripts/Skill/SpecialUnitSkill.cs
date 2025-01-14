using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialUnitSkill : UnitSkill
{
    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {
        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // 이건 스킬마다 바인딩해놓은 유닛을 타겟으로 잡는거
        battleManager.cameraStateController.SwitchToSkillView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함
        // 애니메이터 설정 anim.SetTrigger(블라블라);

        yield return new WaitForSeconds(5f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform);

        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);

        yield return new WaitForSeconds(2f);

        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // 이건 스킬마다 바인딩해놓은 유닛을 타겟으로 잡는거
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함

        yield return new WaitForSeconds(1f);

        changeStateWhenActEnd?.Invoke();
    }
}
