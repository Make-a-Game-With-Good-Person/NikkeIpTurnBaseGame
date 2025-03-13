using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelectController : MonoBehaviour
{
    public LayerMask layerMask;
    public SquadUIManager uiManager;
    private float lastClickTime = 0f;  // 마지막 클릭 시간
    private float clickCooldown = 0.3f; // 클릭 간격 제한 (0.3초)

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < clickCooldown) return;  // 더블클릭 방지
            lastClickTime = Time.time;  // 마지막 클릭 시간 업데이트

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            SelectCharacter(ray);
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)  // 터치가 시작될 때만 처리
            {
                if (Time.time - lastClickTime < clickCooldown) return; // 더블탭 방지
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
