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
    /// �÷��̾�� �����ִ� �� ���ο� ���� ��ư�� ��ȣ�ۿ��� ����ϴ� �Լ�
    /// </summary>
    /// <param name="attackable"> ������ �̹� �� ���� ���� ����</param>
    /// <param name="movable"> ������ �̹� �� �̵� ���� ����</param>
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
