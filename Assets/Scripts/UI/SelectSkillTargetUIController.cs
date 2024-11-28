using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectSkillTargetUIController : MonoBehaviour
{
    #region Properties
    #region Private
    #endregion
    #region Protected
    #endregion
    #region public
    public GameObject SelectSkillTargetUI;
    public Button cancelButton;
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
        SelectSkillTargetUI.SetActive(true);
    }
    public void Hide()
    {
        SelectSkillTargetUI.SetActive(false);
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
