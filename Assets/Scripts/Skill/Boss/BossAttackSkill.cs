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
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��
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
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // �̰� ��Ʋ �Ŵ����� �������� ������ Ÿ������ ��°� ���� �Ȱ��� ��

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "�� ���� ��");
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
                Debug.Log("����");
                if (IsActionCritical())
                {
                    Debug.Log("ũ��Ƽ��!");
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
