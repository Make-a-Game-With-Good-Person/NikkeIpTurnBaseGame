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
        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // �̰� ��ų���� ���ε��س��� ������ Ÿ������ ��°�
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� " + battleManager.selectedTarget.gameObject.name + "�� ���� ����");
        //battleManager.curControlUnit.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);
        //skillVFX.Play();

        yield return StartCoroutine(CheckAttackTarget());
        yield return new WaitForSeconds(2f);

        //battleManager.cameraStateController.SwitchToQuaterView(owner.transform); // �̰� ��ų���� ���ε��س��� ������ Ÿ������ ��°�
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� ���� ��");
        changeStateWhenActEnd?.Invoke();
    }

    IEnumerator CheckAttackTarget()
    {
        yield return null;

        Collider[] targets = Physics.OverlapSphere(battleManager.selectedTarget.transform.position, skillTargetRange, skillLayerMask);

        float dmg = CalculAttackDamage();
        foreach (Collider target in targets)
        {
            if (IsActionAccuracy())
            {
                Debug.Log("����");
                if (target.gameObject.layer == LayerMask.NameToLayer("Cover")) // ���� ���� ��
                {
                    if (IsActionCritical())
                    {
                        Debug.Log("ũ��Ƽ��!");
                        target.GetComponent<IDamage>().TakeDamage(dmg * battleManager.curControlUnit[EStatType.CRIMul]*1.2f);
                    }
                    else
                    {
                        target.GetComponent<IDamage>().TakeDamage(dmg * 1.2f);
                    }
                }
                else // ���� ���� ��
                {
                    if (IsActionCritical())
                    {
                        Debug.Log("ũ��Ƽ��!");
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
