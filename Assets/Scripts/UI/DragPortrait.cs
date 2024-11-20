using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragPortrait : MonoBehaviour
{
    private GameObject instanciatedUnit;
    public GameObject UnitPrefab;
    public UnityEvent<GameObject, Vector3> onDropEvent;

    public void OnBeginDrag()
    {
        instanciatedUnit.SetActive(true);
    }

    public void OnDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, 1 << 4);

        instanciatedUnit.transform.position = hit.point;
    }

    public void OnDragEnd()
    {
        Debug.Log(instanciatedUnit.transform.position);
        onDropEvent?.Invoke(instanciatedUnit, instanciatedUnit.transform.position);
    }

    private void Awake()
    {
        instanciatedUnit = Instantiate(UnitPrefab);
        instanciatedUnit.gameObject.SetActive(false);
    }
}
