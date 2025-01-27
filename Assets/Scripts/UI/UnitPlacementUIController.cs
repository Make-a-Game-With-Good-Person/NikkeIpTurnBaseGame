using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitPlacementUIController : MonoBehaviour
{
    //12-03
    //dragPortraits를 딕셔너리로 바꾸던가 어쩌던가 효율적인 방법이 있긴 할건데 일단 이렇게 짜둠

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
    public UnityEvent<Unit> failToPlaceEvent = new UnityEvent<Unit>();
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
    public void OnFailToPlace(Unit unit)
    {
        failToPlaceEvent?.Invoke(unit);
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
