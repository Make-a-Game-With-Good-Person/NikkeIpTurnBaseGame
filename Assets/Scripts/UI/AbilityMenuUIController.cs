using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityMenuUIController : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region public
    public GameObject AbilityMenuUI;
    public Button abilityButton;
    public Button moveButton;
    public Button turnEndButton;
    #endregion
    #region Events
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
        AbilityMenuUI.SetActive(true);
    }
    public void Hide()
    {
        AbilityMenuUI.SetActive(false);
    }
    
    /// <summary>
    /// 플레이어에게 남아있는 턴 여부에 따라 버튼의 상호작용을 토글하는 함수
    /// </summary>
    /// <param name="attackable"> 유닛의 이번 턴 공격 가능 여부</param>
    /// <param name="movable"> 유닛의 이번 턴 이동 가능 여부</param>
    public void ActivateButtons(bool attackable, bool movable)
    {
        abilityButton.interactable = attackable;
        moveButton.interactable = movable;
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
        Hide();
    }
    #endregion
}
