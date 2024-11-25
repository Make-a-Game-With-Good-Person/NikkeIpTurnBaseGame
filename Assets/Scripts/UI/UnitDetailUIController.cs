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
    public Text selectedUnit_ExpectedDamage; // �̷������� ǥ���ؾ��� ��ġ���� �����Ѵ�.
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
    // �䱸���� 1. �÷��̾�� ĳ���͸� ������ ���¿��� �� UI�� Ű�� �� ������ �� ������ �����ش�.
    // �䱸���� 2. �� ĳ���͸� ������ ���¿��� �� UI�� Ű�� �� ������ �� ������ �����ش�.
    public void Display(Unit selectedUnit)
    {
        UnitDetailUI.SetActive(true);
        // �� �Ʒ��� �ʿ��� ������������ selectedUnit[EstatType.]... �̷������� ������ ���� �ɰ� ����
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
