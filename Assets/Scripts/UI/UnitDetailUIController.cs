using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitDetailUIController : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region public
    public GameObject UnitDetailUI;
    public Text selectedUnit_Hp;
    public Text selectedUnit_ExpectedDamage; // 이런식으로 표기해야할 수치들을 나열한다.
    public Button cancelButton;
    #endregion
    #region Events
    public UnityEvent cancelEvent = new UnityEvent();
    #endregion
    #endregion

    #region Constructor
    #endregion

    #region Methods
    #region Private
    void OnCancle()
    {
        cancelEvent?.Invoke();
    }
    #endregion
    #region Protected
    #endregion
    #region Public
    // 요구사항 1. 플레이어블 캐릭터를 선택한 상태에서 이 UI를 키면 내 유닛의 상세 정보를 보여준다.
    // 요구사항 2. 적 캐릭터를 선택한 상태에서 이 UI를 키면 적 유닛의 상세 정보를 보여준다.
    public void Display(Unit selectedUnit)
    {
        UnitDetailUI.SetActive(true);
        // 이 아래로 필요한 스텟정보들을 selectedUnit[EstatType.]... 이런식으로 가져와 쓰면 될거 같음
    }
    public void Hide()
    {
        UnitDetailUI.SetActive(false);
    }
    #endregion
    #endregion

    #region EventHandlers
    
    #endregion

    #region Coroutines
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        cancelButton.onClick.AddListener(OnCancle);
        Hide();
    }
    #endregion
}
