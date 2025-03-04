using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillEunhwa : UnitSkill
{
    [SerializeField] Unit skillOwner;
    [SerializeField] float damageValue;
    [SerializeField] int stunTurn;
    int curStunTurn;
    bool stunned;


    LayerMask skillLayerMask;
    protected override void Start()
    {
        base.Start();
        skillLayerMask = (1 << 8) | (1 << 9);
        curStunTurn = stunTurn;
        battleManager.PlayerRoundEndEvent.AddListener(ResetState);
    }

    public override void TurnOnTargetRange()
    {
        Vector3 attackDirection = (battleManager.selectedTarget.transform.position - battleManager.curControlUnit.transform.position).normalized;
        Vector3 boxCenter = battleManager.selectedTarget.transform.position + attackDirection * (skillTargetRange / 2);
        Vector3 boxSize = new Vector3(1, skillHeight, skillTargetRange);
        Quaternion boxRotation = Quaternion.LookRotation(attackDirection, Vector3.up);

        this.skillTargetRangeVFX.transform.position = boxCenter;
        this.skillTargetRangeVFX.transform.localScale = boxSize;
        this.skillTargetRangeVFX.transform.rotation = boxRotation;

        skillTargetRangeVFX.gameObject.SetActive(true);
    }

    public override void Action()
    {
        base.Action();
        StartCoroutine(SkillAction());
    }
    IEnumerator SkillAction()
    {
        stunned = true;
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함
        // 애니메이터 설정 anim.SetTrigger(블라블라);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 " + battleManager.selectedTarget.gameObject.name + "를 향한 스폐셜 공격");
        yield return new WaitForSeconds(2f); // 애니메이션 클립의 재생시간만큼 딜레이를 주면 됨

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);
        yield return StartCoroutine(CheckPenetratedTarget());
        yield return new WaitForSeconds(2f);

        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform); // 이건 배틀 매니저가 제어중인 유닛을 타겟으로 잡는거 둘이 똑같긴 함

        yield return new WaitForSeconds(1f);
        Debug.Log(battleManager.curControlUnit.gameObject.name + "의 공격 끝");

        skillOwner.TurnEnd();
        skillOwner.isStunned = true;
        changeStateWhenActEnd?.Invoke();
    }

    void ResetState()
    {
        if (!stunned) return;

        curStunTurn--;
        if (curStunTurn == 0)
        {
            curStunTurn = stunTurn;
            stunned = false;
            skillOwner.isStunned = false;
        }

    }

    IEnumerator CheckPenetratedTarget()
    {
        yield return null;

        Vector3 attackDirection = (battleManager.selectedTarget.transform.position - battleManager.curControlUnit.transform.position).normalized;
        Vector3 boxCenter = battleManager.selectedTarget.transform.position + attackDirection * (skillTargetRange / 2);
        Vector3 boxSize = new Vector3(2, skillHeight, skillTargetRange);
        Quaternion boxRotation = Quaternion.LookRotation(attackDirection, Vector3.up);

        Collider[] targets = Physics.OverlapBox(boxCenter, boxSize / 2, boxRotation, skillLayerMask);
        
        foreach (Collider target in targets)
        {
            if (IsActionAccuracy())
            {
                float dmg = CalculAttackDamage();
                Debug.Log("명중");
                if (IsActionCritical())
                {
                    Debug.Log("크리티컬!");
                    target.GetComponent<IDamage>().TakeDamage(dmg * battleManager.curControlUnit[EStatType.CRIMul] * damageValue);
                }
                else
                {
                    target.GetComponent<IDamage>().TakeDamage(dmg * damageValue);
                }
            }
        }

        DebugDrawBox(boxCenter, boxSize, boxRotation, Color.red, 2f);
    }

    #region ForDebug
    // 디버그 박스 그리기
    private void DebugDrawBox(Vector3 center, Vector3 size, Quaternion rotation, Color color, float duration)
    {
        Matrix4x4 matrix = Matrix4x4.TRS(center, rotation, size);
        Vector3[] corners = {
            matrix.MultiplyPoint3x4(new Vector3(-0.5f, -0.5f, -0.5f)),
            matrix.MultiplyPoint3x4(new Vector3( 0.5f, -0.5f, -0.5f)),
            matrix.MultiplyPoint3x4(new Vector3( 0.5f, -0.5f,  0.5f)),
            matrix.MultiplyPoint3x4(new Vector3(-0.5f, -0.5f,  0.5f)),
            matrix.MultiplyPoint3x4(new Vector3(-0.5f,  0.5f, -0.5f)),
            matrix.MultiplyPoint3x4(new Vector3( 0.5f,  0.5f, -0.5f)),
            matrix.MultiplyPoint3x4(new Vector3( 0.5f,  0.5f,  0.5f)),
            matrix.MultiplyPoint3x4(new Vector3(-0.5f,  0.5f,  0.5f))
        };

        Debug.DrawLine(corners[0], corners[1], color, duration);
        Debug.DrawLine(corners[1], corners[2], color, duration);
        Debug.DrawLine(corners[2], corners[3], color, duration);
        Debug.DrawLine(corners[3], corners[0], color, duration);
        Debug.DrawLine(corners[4], corners[5], color, duration);
        Debug.DrawLine(corners[5], corners[6], color, duration);
        Debug.DrawLine(corners[6], corners[7], color, duration);
        Debug.DrawLine(corners[7], corners[4], color, duration);
        Debug.DrawLine(corners[0], corners[4], color, duration);
        Debug.DrawLine(corners[1], corners[5], color, duration);
        Debug.DrawLine(corners[2], corners[6], color, duration);
        Debug.DrawLine(corners[3], corners[7], color, duration);
    }
    #endregion
}
