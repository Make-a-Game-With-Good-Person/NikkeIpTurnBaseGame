using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkillEmma : UnitSkill
{
    [SerializeField] Unit target;
    [SerializeField] float minusRange;
    int deBuffDur;
    bool deBuffOn;
    public float DefaultRange;
    public GameObject deBuffVFX;

    protected override void Start()
    {
        base.Start();
        deBuffDur = 1;
        battleManager.EnemyRoundEndEvent.AddListener(ResetStat);
    }

    public override void Action()
    {
        base.Action();
        target = battleManager.selectedTarget.GetComponent<Unit>();
        DefaultRange = target[EStatType.Visual];
        minusRange = DefaultRange / 2;
        StartCoroutine(SkillAction());
    }
    IEnumerator SkillAction()
    {
        deBuffOn = true;
        target[EStatType.Visual] -= minusRange;
        Debug.Log($"{target.name}의 사정거리가 {target[EStatType.Visual]}로 감소");
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.selectedTarget.transform);
        if(deBuffVFX != null) Instantiate(deBuffVFX, new Vector3(target.transform.position.x, target.transform.position.y + 1f, target.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        battleManager.cameraStateController.SwitchToQuaterView(battleManager.curControlUnit.transform);
        changeStateWhenActEnd?.Invoke();
    }

    void ResetStat()
    {
        if (!deBuffOn) return;
        deBuffDur--;
        Debug.Log($"남은 버프턴 {deBuffDur}");
        if (deBuffDur <= 0)
        {
            deBuffDur = 1;
            target[EStatType.Visual] += minusRange;
            Debug.Log($"{target.name}의 사정거리가 {target[EStatType.Visual]}로 롤백");
            deBuffOn = false;
        }
    }
}
