using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelectController : MonoBehaviour
{
    public LayerMask layerMask;
    public SquadUIManager uiManager;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // ���콺 ��ġ���� ���̸� ��
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))  // ���̰� �´��� Ȯ��
            {
                hit.transform.GetComponent<SquadUICharacter>().SelectCharacter();
            }
        }
#endif
#if UNITY_ANDROID
#endif
    }
}
