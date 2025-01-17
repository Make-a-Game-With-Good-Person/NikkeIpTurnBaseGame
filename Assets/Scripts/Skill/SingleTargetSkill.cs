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
        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // �̰� ��ų���� ���ε��س��� ������ Ÿ������ ��°�
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��
        // �ִϸ����� ���� anim.SetTrigger(�����);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� " + battleManager.selectedTarget.gameObject.name + "�� ���� ����");
        //battleManager.curControlUnit.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);
        if (IsActionAccuracy())
        {
            Debug.Log("����");
            if (IsActionCritical())
            {
                Debug.Log("ũ��Ƽ��!");
                battleManager.selectedTarget.GetComponent<IDamage>().TakeDamage(
                    battleManager.curControlUnit[EStatType.ATK] * battleManager.curControlUnit[EStatType.CRIMul]);
            }
            else
            {
                battleManager.selectedTarget.GetComponent<IDamage>().TakeDamage(battleManager.curControlUnit[EStatType.ATK]);
            }
        }
        yield return new WaitForSeconds(2f);

        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // �̰� ��ų���� ���ε��س��� ������ Ÿ������ ��°�
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� ���� ��");
        changeStateWhenActEnd?.Invoke();
    }
}
