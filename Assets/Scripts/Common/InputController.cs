using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region public
    public LayerMask GroundMask;
    #endregion
    #region Events
    public UnityEvent<Vector3> mouseOverEvent = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> touchEvent = new UnityEvent<Vector3>();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    private Vector3 GetWorldPositionFromMouse(LayerMask layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask);

        if(hit.collider == null)
        {
            return new Vector3(-1, -1, -1);
        }

        return hit.point;
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    #endregion
    #endregion

    #region EventHandlers
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Update()
    {
        //임시로 레이어는 4번 레이어
        mouseOverEvent?.Invoke(GetWorldPositionFromMouse(GroundMask));
        if (Input.GetMouseButtonDown(0))
        {
            touchEvent?.Invoke(GetWorldPositionFromMouse(GroundMask));
        }
    }
    #endregion

    #region ForTest
    #endregion
}
