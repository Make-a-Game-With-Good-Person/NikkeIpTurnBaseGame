using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragPortrait : MonoBehaviour
{
    private Unit instanciatedUnit;
    public Unit UnitPrefab;
    public UnityEvent<DragPortrait> DragStartEvent = new UnityEvent<DragPortrait>();
    public UnityEvent<Unit, Vector3> onDropEvent = new UnityEvent<Unit, Vector3>();

    public void OnBeginDrag()
    {
        instanciatedUnit.gameObject.SetActive(true);
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

    public void OnFailToPlace(Unit unit)
    {
        if(instanciatedUnit == unit)
            instanciatedUnit.gameObject.SetActive(false);
    }

    private void Awake()
    {
        instanciatedUnit = Instantiate(UnitPrefab);
        instanciatedUnit.gameObject.SetActive(false);
    }
}
