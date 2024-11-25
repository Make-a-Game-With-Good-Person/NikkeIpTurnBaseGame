using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitCamSetting : MonoBehaviour
{
    protected Unit unit;
    public UnityEvent unitSelectCamEvent;

    private void Start()
    {
        unit = GetComponent<Unit>();
    }

    #region Mouse Events
    private void OnMouseDown()
    {
        unitSelectCamEvent?.Invoke();
    }
    #endregion
}
