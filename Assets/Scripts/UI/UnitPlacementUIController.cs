using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitPlacementUIController : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region public
    public GameObject UnitPlaceUI;
    public Button confirmButton;
    #endregion
    #region Events
    public UnityEvent<Unit, Vector3> setUnitEvent = new UnityEvent<Unit, Vector3>();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    #endregion
    #region Protected
    #endregion
    #region Public
    public void Display()
    {
        UnitPlaceUI.SetActive(true);
    }
    public void Hide()
    {
        UnitPlaceUI.SetActive(false);
    }
    #endregion
    #endregion

    #region EventHandlers
    public void OnDragImageEnd(Unit unit, Vector3 worldPos)
    {
        setUnitEvent?.Invoke(unit, worldPos);
    }
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        Hide();
    }
    #endregion
}
