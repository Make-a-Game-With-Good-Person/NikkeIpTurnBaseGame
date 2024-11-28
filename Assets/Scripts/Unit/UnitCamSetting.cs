using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitCamSetting : MonoBehaviour
{
    public UnityEvent unitSelectCamEvent;

    #region Mouse Events
    private void OnMouseDown()
    {
        Debug.Log(this.gameObject.name + "Å¬¸¯!");
        unitSelectCamEvent?.Invoke();
    }

    #endregion
}
