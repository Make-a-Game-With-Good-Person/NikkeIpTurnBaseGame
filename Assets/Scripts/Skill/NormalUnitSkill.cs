using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUnitSkill : UnitSkill
{
    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {
        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // �̰� ��ų���� ���ε��س��� ������ Ÿ������ ��°�
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��
        // �ִϸ����� ���� anim.SetTrigger(�������);

        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);

        yield return new WaitForSeconds(2f);

        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // �̰� ��ų���� ���ε��س��� ������ Ÿ������ ��°�
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��

        yield return new WaitForSeconds(1f);

        changeStateWhenActEnd?.Invoke();
    }
}