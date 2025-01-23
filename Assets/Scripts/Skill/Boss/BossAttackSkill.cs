using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSkill : UnitSkill
{
    [SerializeField] int coolTime;
    public int curCoolTime = 0;
    void CoolTimeCheck()
    {
        curCoolTime++;
        if (curCoolTime >= coolTime) curCoolTime = 0;
    }

    protected override void Start()
    {
        base.Start();

        battleManager.RoundEndEvent.AddListener(CoolTimeCheck);
    }

    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }

    IEnumerator SkillAction()
    {
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

}
