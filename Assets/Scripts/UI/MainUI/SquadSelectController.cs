using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelectController : MonoBehaviour
{
    public LayerMask layerMask;
    public SquadUIManager uiManager;
    private float lastClickTime = 0f;  // ������ Ŭ�� �ð�
    private float clickCooldown = 0.3f; // Ŭ�� ���� ���� (0.3��)

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < clickCooldown) return;  // ����Ŭ�� ����
            lastClickTime = Time.time;  // ������ Ŭ�� �ð� ������Ʈ

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SelectCharacter(ray);
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)  // ��ġ�� ���۵� ���� ó��
            {
                if (Time.time - lastClickTime < clickCooldown) return; // ������ ����
                lastClickTime = Time.time;

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                SelectCharacter(ray);
            }
        }
#endif
    }

    void SelectCharacter(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            SquadUICharacter character = hit.transform.GetComponent<SquadUICharacter>();
            if (character != null)
            {
                character.SelectCharacter();
            }
        }
    }
}
