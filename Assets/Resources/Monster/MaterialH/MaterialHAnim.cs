using System.Collections;
using UnityEngine;

public class MaterialHAnim : MonoBehaviour
{
    private Animator animator;
    private float chance = 0.3f;

    // 코루틴을 선택하기 위한 Enum
    public enum CoroutineType { MaterialH, Ele }
    public CoroutineType selectedCoroutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        switch (selectedCoroutine)
        {
            case CoroutineType.MaterialH:
                animator.SetBool("isMaterialH", true);
                StartCoroutine(ActivateRomingTrigger());
                break;

            case CoroutineType.Ele:
                animator.SetBool("isMaterialH", false);
                StartCoroutine(ChangeAnimationParameter());
                break;
        }
    }

    private IEnumerator ActivateRomingTrigger()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (Random.value <= chance)
            {
                animator.SetTrigger("Duribun");
                //Debug.Log("'Roming' 트리거 활성화!");
            }
        }
    }

    private IEnumerator ChangeAnimationParameter()
    {
        while (true)
        {
            float waitTime = Random.Range(1.5f, 2.5f);
            yield return new WaitForSeconds(waitTime);

            int randomValue = Random.Range(1, 4);
            animator.SetInteger("Pos", randomValue);
        }
    }
}
